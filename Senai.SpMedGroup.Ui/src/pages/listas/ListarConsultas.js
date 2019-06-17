import React, { Component } from "react";
import Table from "../../../node_modules/react-bootstrap/Table"
import "../../../node_modules/bootstrap/dist/css/bootstrap.css"

class ListarConsultas extends Component {

    constructor() {
        super();
        this.state = {
            lista: [],
            nome: "",
            tituloPagina: "Lista Consultas"
        }
    }

    buscarConsulta() {
        fetch('http://192.168.5.46:5000/api/consultas', {
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
        this.buscarConsulta();
    }
s
    listarConsulta(event) {
        event.preventDefault();


        fetch('http://192.168.5.46:5000/api/consultas',
            {
                method: 'GET',
                body: JSON.stringify({ nome: this.state.nome }),
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": 'Bearer ' + localStorage.getItem("token-autenticacao")
                }
            })
            .then(resposta => resposta)
            .then(this.buscarConsulta())
            .catch(erro => console.log(erro))
    }

    render() {
        return (
            <div className="table">
                <Table striped bordered hover>
                    <thead>
                        <tr>
                            <th>ID Consulta</th>
                            <th>Medico</th>
                            <th>Data da Consulta</th>
                            <th>Situação</th>
                            <th>Descrição</th>
                        </tr>
                    </thead>
                    <tbody>
                        {
                            this.state.lista.map(consulta => {
                                return (
                                    <tr key={consulta.idConsulta}>
                                        <td>{consulta.idConsulta}</td>
                                        <td>{consulta.idMedicoNavigation.nome}</td>
                                        <td>{consulta.dtConsulta}</td>
                                        <td>{consulta.situacao}</td>
                                        <td>{consulta.descricao}</td>
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
export default ListarConsultas;