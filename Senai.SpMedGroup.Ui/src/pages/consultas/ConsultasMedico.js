import React, { Component } from "react";
import { Link } from "react-router-dom";
import { logout } from "../../services/logout";
import Table from "../../../node_modules/react-bootstrap/Table"
import "../../../node_modules/bootstrap/dist/css/bootstrap.css"

class ConsultasMedico extends Component {
    constructor() {
        super();

        this.state = {
            consultas: []
        }
    }

    // carrega o metodo
    componentDidMount() {
        this.listarConsultas();
    }

    // lista todas as consultas
    listarConsultas() {
        fetch('http://192.168.5.46:5000/api/consultas', {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
                "Authorization": 'Bearer ' + localStorage.getItem("token-autenticacao")
            }
        })
            .then(resposta => resposta.json())
            .then(data => this.setState({ consultas: data }))
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
                            this.state.consultas.map(consulta => {
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
                    <a href="/">Sair</a>
                </div>

            </div>
        );
    }
}

export default ConsultasMedico;