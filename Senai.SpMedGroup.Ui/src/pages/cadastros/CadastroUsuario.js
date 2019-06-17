import React, { Component } from "react";
import Axios from "axios";
import apiService from "../../services/apiService";

class CadastroUsuario extends Component {

    constructor() {
        super();

        this.state = {
            nome: "",
            email: "",
            senha: "",
            tipoUsuario: "",
        }

        this.atualizarNome = this.atualizarNome.bind(this);
        this.atualizarEmail = this.atualizarEmail.bind(this);
        this.atualizarSenha = this.atualizarSenha.bind(this);
        this.atualizarTipoUsuario = this.atualizarTipoUsuario.bind(this);
    }

    //Pegar o valor que usuario digitar
    atualizarNome(event) {
        this.setState({ nome: event.target.value });
    }

    atualizarEmail(event) {
        this.setState({ email: event.target.value });
    }

    atualizarSenha(event) {
        this.setState({ senha: event.target.value });
    }
    atualizarTipoUsuario(event) {
        this.setState({ tipoUsuario: event.target.value });
    }

    cadastrarUsuario(event) {
        event.preventDefault();
        alert("Usuario Cadastrado");
        window.location = '/CadastroUsuario'

        let usuario = {
            nome: this.state.nome,
            email: this.state.email,
            senha: this.state.senha,
            tipoUsuario: this.state.tipoUsuario,
        }

        Axios({
            method: 'POST',
            url: 'http://192.168.5.46:5000/api/usuarios',
            data: usuario,
            headers: {
                "Content-Type": "application/json",
                "Authorization": 'Bearer ' + localStorage.getItem("token-autenticacao")
            }
        })
            .catch(erro => {
                this.setState({ erroMensagem: 'Usuario Cadastrado' });
            })
    }

    componentDidMount() {
        apiService
            .call("cadastro")
            .getAll()
            .then(data => {
                this.setState({ listaUsuario: data.data });
            });
    }


    render() {
        return (
            <div className="container">
                <div class="content2">
                    <div id="cadastro">
                        <form onSubmit={this.cadastrarUsuario.bind(this)}>
                            <h1>Cadastro de Usuarios</h1>

                            <p>
                                <label>Nome</label>
                                <input type="text" required="required" value={this.state.nome} onChange={this.atualizarNome} />
                            </p>

                            <p>
                                <label for="email">Email</label>
                                <input type="text" required="required" value={this.state.email} onChange={this.atualizarEmail} />
                            </p>

                            <p>
                                <label for="senha">Senha</label>
                                <input type="password" required="required" value={this.state.senha} onChange={this.atualizarSenha} />
                            </p>
                            <div className="tipo_usuario">
                                <label for="tipo_usuario">Tipo Usuario</label>
                                <select name="tipo_usuario" required value={this.state.tipoUsuario} onChange={this.atualizarTipoUsuario}>
                                    <option value="" selected disabled hidden>Selecione</option>
                                    <option value="1" defaultValue>Administrador</option>
                                    <option value="2" defaultValue>Medico</option>
                                    <option value="3" defaultValue>Paciente</option>
                                </select>
                            </div>

                            <input type="submit" value="Cadastrar" />
                            <p className="sucesso" style={{ color: 'green', textAlign: 'center' }}>{this.state.erroMensagem}</p>

                            <div className="voltar">
                                <a href="/App">Voltar</a>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        )
    }
}

export default CadastroUsuario;