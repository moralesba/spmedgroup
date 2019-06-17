import React, { Component } from "react";
import Axios from "axios";
import { parseJwt } from "../../services/auth";

class Login extends Component {
    constructor() {
        super();

        this.state = {
            email: "",
            senha: "",
            erroMensagem : ""
        }

        this.atualizarEmail = this.atualizarEmail.bind(this);
        this.atualizarSenha = this.atualizarSenha.bind(this);
    }

    atualizarEmail(event) {
        this.setState({ email: event.target.value });
    }

    atualizarSenha(event) {
        this.setState({ senha: event.target.value });
    }

    efetuaLogin(event) {
        event.preventDefault();

        Axios.post('http://192.168.5.46:5000/api/login', {
            email: this.state.email,
            senha: this.state.senha
        })
            .then(data => {
                if (data.status === 200) {
                    console.log(data);
                    localStorage.setItem("token-autenticacao", data.data.token);
                    console.log(parseJwt().UsuarioTipo);
                    if (parseJwt().UsuarioTipo == "Administrador") {
                        this.props.history.push("/App");
                    } else if (parseJwt().UsuarioTipo == "Medico") {
                        this.props.history.push("/ConsultasMedico");
                    } else if (parseJwt().UsuarioTipo == "Paciente") {
                        this.props.history.push("/ConsultasPaciente");
                    }
                };
            })
            .catch(erro => {
                this.setState({ erroMensagem : 'Email ou senha inválido'});
            })
        }

    render() {
        return (
            <div class="container" >
                <a className="links" id="paracadastro"></a>
                <a className="links" id="paralogin"></a>

                <div class="content1">
                    <div id="login">
                        <form onSubmit={this.efetuaLogin.bind(this)}>
                            <h1>Login</h1>
                            <p>
                                <label for="nome_login">Email: </label>
                                <input id="email" name="email" required="required" type="text" placeholder="exemplo@gmail.com" value={this.state.email} onChange={this.atualizarEmail} />
                            </p>
                            <p>
                                <label for="senha">Senha: </label>
                                <input id="senha" name="senha" required="required" type="password" placeholder="********" value={this.state.senha} onChange={this.atualizarSenha} />
                            </p>
                            <p>
                                <input type="submit" value="Entrar" />
                            </p>
                            <p className="text__login" style={{ color : 'red',  textAlign : 'center' }}>{this.state.erroMensagem}</p>
                         </form>
                    </div>
                </div>
            </div>
        )
    }
}

export default Login;