import React, { Component } from "react";
import { logout } from "../../services/logout";
import { Link } from "react-router-dom";
import "../home/App.css";

class App extends Component {

    render() {
        return (
            <div class="container">
                <div class="content2">
                    <h1>Home</h1>
                    <div class="links">
                        <a href="/ListarConsultas">Lista de consultas</a>
                        <a href="/ListarUsuarios">Lista de usuarios</a>
                        <a href="/CadastroUsuario">Cadastrar um usuario</a>
                        <a href="/CadastroConsulta">Cadastrar uma consulta</a>
                    </div>
                    <div className="voltar">
                        <a href="/">Sair</a>
                    </div>
                </div>

            </div>
        )
    }
}
export default App;