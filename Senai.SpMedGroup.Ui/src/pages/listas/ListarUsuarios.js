import React, { Component } from "react";
import Table from "../../../node_modules/react-bootstrap/Table"
import "../../../node_modules/bootstrap/dist/css/bootstrap.css"

class ListarUsuarios extends Component {

    constructor() {
        super();
        this.state = {
            lista: [],
            nome: "",
            tituloPagina: "Lista Usuarios"
        }
    }

    buscarUsuarios() {
        fetch('http://192.168.5.46:5000/api/usuarios', {
            method: 'GET',
            headers: {
                "Content-Type": "application/json",
                "Authorization": 'Bearer ' + localStorage.getItem("token-autenticacao")
            }
        })
            .then(resposta => resposta.json())
            .then(data => this.setState({ lista: data }))
            .catch((erro) => console.log(erro))
    }

    componentDidMount() {
        this.buscarUsuarios();
    }

    cadastraUsuario(event) {
        event.preventDefault();


        fetch('http://192.168.5.46:5000/api/usuarios',
            {
                method: 'POST',
                body: JSON.stringify({ nome: this.state.nome }),
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": 'Bearer ' + localStorage.getItem("token-autenticacao")
                }
            })
            .then(resposta => resposta)
            .then(this.buscarUsuarios())
            .catch(erro => console.log(erro))
    }

    render() {
        return (
            <div className="table">
                <Table striped bordered hover>
                    <thead>
                        <tr>
                            <th>Nome</th>
                            <th>Email</th>
                            <th>Senha</th>
                            <th>Tipo Usuario</th>
                        </tr>
                    </thead>
                    <tbody>
                        {
                            this.state.lista.map(function (usuario) {
                                return (
                                    <tr key={usuario.idUsuario}>
                                        <td>{usuario.nome}</td>
                                        <td>{usuario.email}</td>
                                        <td>{usuario.senha}</td>
                                        <td>{usuario.tipoUsuarioNavigation.nome}</td>
                                    </tr>
                                );
                            })
                        }
                    </tbody>
                </Table>
                <div className="voltar">
                    <a href="/App">Voltar</a>
                </div>
            </div>
        );
    }
}
export default ListarUsuarios;