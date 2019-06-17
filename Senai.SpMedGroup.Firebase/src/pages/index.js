import React from 'react';
import firebase from "../services/firebaseConfig";
import ValidarDados from "../services/validarDados";
import { Map, GoogleApiWrapper, Marker } from 'google-maps-react';
import { Form, Button, Col, Row, Container, Alert, FormGroup } from 'react-bootstrap'
import CardLocalizacao from "../componentes/Localizacao";

const dadosUsuario = "dados-usuario";
const especialidades = "especialidade";

class Home extends React.Component {
    constructor(props) {
		super(props);
		this.state = {
			nome: "",
			latitude: 0,
			longitude: 0,
			medico:"",
			especialidade: "",
            descricao: "",
			dtNascimento:new Date(),
	
			listaDados: [],
			listaEspecialidades: [],
			
			loading: false,
			erro: null,
			sucesso: null,
			
			usuario:{}
		}
	}

	componentDidMount() {
		this._listarEspecialidades();
		this._listarDados();
		this._validarUsuario();
	}

	_listarDados() {
		firebase.firestore().collection(dadosUsuario)
			.onSnapshot((dados) => {
				let array = [];
				dados.forEach((dado) => {
					array.push({
						nome: dado.data().Nome,
						latitude: dado.data().localizacao.latitude,
						longitude: dado.data().localizacao.longitude,
						medico:{
							nome:dado.data().medico.nome,
							especialidade: dado.data().medico.especialidade,						
						},
                        descricao: dado.data().descricao,
                        idade: dado.data().idade
					})
				})
				console.log(dados)
				this.setState({ listaDados: array })
			})
	}

	_validarUsuario(){
		
        firebase.auth().onAuthStateChanged(
            usuario=>{
                if(usuario){
                    this.setState({
                        usuario
                    });
                }else{
                    this.setState({
                        usuario:null
                    });
                }
            }
        )
	}

	_limparDados() {
		this.setState(
			{
				nome: "",
				latitude: 0,
				longitude: 0,
				medico:"",
				especialidade: "",
                descricao: "",
                dtNascimento:new Date(),
				sucesso: null,
				erro: null,
				loading: false
			}
		)
	}
	
	_listarEspecialidades(){
		firebase.firestore().collection(especialidades)
		.get()
		.then((dados) => {
			let array = [];
			dados.forEach((dado) => {
				array.push({
					id:dado.data().id,
					nome: dado.data().nome,
				})
			})
			this.setState({ listaEspecialidades: array })
		}).catch(i => console.error(i))
	}

	_cadastrarDados(event) {
		event.preventDefault();
		this._limparDados();

		const dados =
		{
			Nome: this.state.nome,
			Localizacao: new firebase.firestore.GeoPoint(parseFloat(this.state.latitude), parseFloat(this.state.longitude)),
			Medico:{
				Nome:this.state.medico,
				Especialidade: this.state.especialidade
			},
            Descricao: this.state.descricao.trim(),
            Idade:new Date().getFullYear() - new Date(this.state.dtNascimento).getFullYear() 
		}

		const validar = ValidarDados(dados);
		if (!validar.erro) {
			firebase.firestore()
				.collection(dadosUsuario)
				.add(dados)
				.then(() => {
					this.setState({
						sucesso: validar.mensagem,
						erro: null
					});
				}).catch(error => {
					console.error(error)
					this.setState({
						sucesso: null,
						erro: validar.mensagem
					});
				})
		} else {
			this.setState({
				sucesso: null,
				erro: validar.mensagem
			});
		}
		this.setState(
			{
				loading: false
			}
		);
	}

	_atualizaEstado(data) {
		this.setState({ [data.target.name]: data.target.value })
	}
	
    _login(e) {
        e.preventDefault();
        this.setState({loading:true})
        firebase.auth()
            .signInWithEmailAndPassword(this.state.email, this.state.senha)
            .then(()=> {
                this.setState(
					{
						loading:false,
						erro:null
					}
				)
            })
            .catch((erro) => {
                this.setState({loading:false})
                switch (erro.code) {
                    case "auth/user-not-found":
                        this.setState({ erro: "Email não cadastrado" })
                        break;
                    case "auth/invalid-email":
                        this.setState({ erro: "Email incorreto" })
                        break;        
                    case "auth/wrong-password":
                            this.setState({ erro: "Senha incorreta" })
                            break;
                    default:
                        this.setState({ erro: erro.message })
                        break;
                }
            });
        
	}

	_formLogin(erro) {
        return (
            <Form onSubmit={this._login.bind(this)}>
                <Form.Group>
                    <Form.Label>Email</Form.Label>
                    <Form.Control
                        type="email"
                        name="email"
                        placeholder="Email"
                        onChange={(e) => this.setState({ email: e.target.value })}
                        value={this.state.email}
                        disabled={this.state.loading}
                        required
                    />
                </Form.Group>
                <Form.Group>
                    <Form.Label>Senha</Form.Label>
                    <Form.Control
                        type="password"
                        name="senha"
                        placeholder="Senha"
                        onChange={(e) => this.setState({ senha: e.target.value })}
                        value={this.state.senha}
                        disabled={this.state.loading}
                        required
                    />
                </Form.Group>
                <Form.Group className="d-flex justify-content-between">
                    <Button 
                        variant='outline-dark' 
                        type="submit" 
                        disabled={this.state.loading}
                    >
                        {
                            this.state.loading?"Entrando":"Login"
                        }
                    </Button>
                </Form.Group>
                {
                     erro
                }
            </Form>
        )
	}
	
	_formCadastro(erro){
		const sucesso = this.state.sucesso !== null ? <Alert variant='success'>{this.state.sucesso}</Alert> : null;
		return(
		<Form onSubmit={this._cadastrarDados.bind(this)} >
			<Form.Group>
			<Form.Group as={Col}>
				<h2>Paciente</h2>
				<Form.Group>
					<Form.Label>Nome:</Form.Label>
					<Form.Control
						type="text"
						maxLength={400}
						name="nome"
						value={this.state.nome}
						onChange={this._atualizaEstado.bind(this)}
						placeholder="Nome"
						required
					/>
				</Form.Group>
				</Form.Group>
				<Form.Row as={Col}>
					<Form.Group as={Col}>
						<Form.Label>Latitude:</Form.Label>
						<Form.Control
							type="number"
							min={-90} max={90}
							step={0.0001}
							name="latitude"
							value={this.state.latitude}
							onChange={this._atualizaEstado.bind(this)}
							placeholder="Latitude"
							required
						/>
						<Form.Text className="text-muted">
							Insira um valor entre -90 e 90
						</Form.Text>
					</Form.Group>
				<Form.Group as={Col}>
					<Form.Label>Longitude:</Form.Label>
					<Form.Control
						type="number"
						min={-180}
						max={180}
						step={0.0001}	
						name="longitude"
						value={this.state.longitude}
						onChange={this._atualizaEstado.bind(this)}
						placeholder="Longitude"
						required
					/>
					<Form.Text className="text-muted">
						Insira um valor entre -180 e 180
					</Form.Text>
				</Form.Group>
				
				<Form.Group as={Col}>
					<Form.Label>Data Nascimento</Form.Label>
					<Form.Control
						type="date"
						name="dtNascimento"
						value={this.state.dtNascimento}
						onChange={this._atualizaEstado.bind(this)}
						required
					/>
				</Form.Group>
			</Form.Row>
			</Form.Group>
			<FormGroup>
				<FormGroup as={Col}>
					<h2>Medico</h2>
					<Form.Row>
						<Form.Group as={Col}>
							<Form.Label>Nome:</Form.Label>
							<Form.Control
								type="text"
								name="medico"
								maxLength={400}
								value={this.state.medico}
								onChange={this._atualizaEstado.bind(this)}
								placeholder="Medico"
								required
							/>
						</Form.Group>
						<Form.Group as={Col}>
							<Form.Label>Especialização:</Form.Label>
							{this._verificarCampos()}
						</Form.Group>
					</Form.Row>
				</FormGroup>
				<Form.Row as={Col}>
					<Form.Group as={Col}>
						<Form.Label>Descrição:</Form.Label>
						<Form.Control
							as='textarea'
							type="text"
							name="descricao"
							value={this.state.descricao}
							onChange={this._atualizaEstado.bind(this)}
							placeholder="Descrição"
						/>
					</Form.Group>
				</Form.Row>
				<Form.Group as={Col}>
				<Button
					variant="outline-dark"
					type="submit"
					size="lg"
					disabled={this.state.loading}
				>
					{
						this.state.loading ? "Carregando" : "Cadastrar"
					}
				</Button>
				<Button 
					variant='outline-danger'
					size="lg"
					className="offset-10"
					disabled={this.state.loading}
					onClick={()=>firebase.auth().signOut()}
				>Logout</Button>
			</Form.Group>				
			</FormGroup>
			{erro}
			{sucesso}
		</Form>
		);
	}

	_verificarCampos(){
		const {listaEspecialidades} = this.state;
		if(listaEspecialidades.length!==0){
			return(
				<select
					className="custom-select mr-sm-2" 
					id="inlineFormCustomSelect"
					name="especialidade" 
					value={this.state.especialidade} 
					onChange={this._atualizaEstado.bind(this)}
				>
					{
						listaEspecialidades.map( i=>{
							return(
								<option key={i.id}>{i.nome}</option>
							)
						}
						)
					}
				</select>
			)
		}else{
			return(
				<Form.Control
					type="text"
					name="especialidade"
					maxLength={400}
					value={this.state.especialidade}
					onChange={this._atualizaEstado.bind(this)}
					placeholder="Especialidade"
					required
				/>
			);
		}
	}

	render() {
		console.log(this.state.usuario)
		const { listaDados } = this.state;
		const erro = this.state.erro !== null ? <Alert variant='danger'>{this.state.erro}</Alert> : null;
        const formulario = this.state.usuario ?
        (<div><h1 className="text-center">Cadastrar Dados</h1>{this._formCadastro(erro)}</div>):
        (<div className="mx-auto w-25"><h1 className="text-center">Login</h1>{this._formLogin(erro)}</div>) ;
		return (
			<div>
				<div className="jumbotron">
					{
						formulario
					}
					<Container>
						<Row>
							{
								listaDados.map(function (dados, key) {
									return (
										<Col
											md={{ span: 4, offset: 1 }}
											key={key}
										>
											<CardLocalizacao id={key} dados={dados} />
										</Col>
									)
								})}
						</Row>
					</Container>
				</div>
				<div>
					<Map
						google={this.props.google}
						zoom={4} style={styleMapa}
						initialCenter=
						{
							{
								lat: -23.5411284, lng: -46.641581
							}
						}
					>
						{
							this.state.listaDados.map(
								(dados, key) => {
									return (
										<Marker
											key={key}
											name={dados.nome}
											position={
												{
													lat: dados.latitude, lng: dados.longitude
												}
											}
										/>
									);
								}
							)
						}
					</Map>
				</div>
			</div>
		)
	}
}

const styleMapa = {
	width: '100%',
	height: '100%',
};


export default GoogleApiWrapper({
	apiKey: 'AIzaSyDQI_JrqQulbqoxyCALVJtdHIKr_KHfv1A'
})(Home);