import React, { Component } from 'react';
import "../../assets/css/consulta.css";
import Axios from "axios";

class CadastroConsulta extends Component {
    constructor() {
        super();

        this.state = {
            dtConsulta: "",
            idMedico: "",
            idProntuario: "",
            descricao: "",
            situacao: "",
            sucessoMensagem: ""
        }

        this.atualizarDataConsulta = this.atualizarDataConsulta.bind(this);
        this.atualizarIdMedico = this.atualizarIdMedico.bind(this);
        this.atualizarIdProntuario = this.atualizarIdProntuario.bind(this);
        this.atualizarDescricao = this.atualizarDescricao.bind(this);
        this.atualizarSituacao = this.atualizarSituacao.bind(this);
    }

    atualizarDataConsulta(event) {
        this.setState({ dtConsulta: event.target.value });
    }
    atualizarIdMedico(event) {
        this.setState({ idMedico: event.target.value });
    }
    atualizarIdProntuario(event) {
        this.setState({ idProntuario: event.target.value });
    }
    atualizarDescricao(event) {
        this.setState({ descricao: event.target.value });
    }
    atualizarSituacao(event) {
        this.setState({ situacao: event.target.value });
    }

    cadastrarConsulta(event) {
        event.preventDefault();
        alert("Consulta Cadastrada");
        window.location = '/CadastroConsulta'

        let consulta = {
            dtConsulta: this.state.dtConsulta,
            idMedico: this.state.idMedico,
            idProntuario: this.state.idProntuario,
            descricao: this.state.descricao,
            situacao: this.state.situacao
        }

        Axios({
            method: 'POST',
            url: 'http://192.168.5.46:5000/api/consultas',
            data: consulta,
            headers: {
                "Content-Type": "application/json",
                "Authorization": 'Bearer ' + localStorage.getItem("token-autenticacao")
            }
        })
            .catch(erro => {
                this.setState({ erroMensagem: 'Erro' });
            })
    }

    render() {
        return (
            <div className="App">
                <main className="container">
                    <div className="content2">
                        <div className="cadastro">
                            <h1>Cadastro de Consultas</h1>
                            <form className="container" onSubmit={this.cadastrarConsulta.bind(this)}>

                                <p>
                                    <label>Data</label>
                                    <input type="date" id="dt" value={this.state.dtConsulta} onChange={this.atualizarDataConsulta} />
                                </p>
                                <div className="medico">
                                    <label>Médico</label>
                                    <select name="medico" required value={this.state.idMedico} onChange={this.atualizarIdMedico}>
                                        <option value="" selected disabled hidden>Selecione</option>
                                        <option value="1" defaultValue>Helena Strada</option>
                                        <option value="2" defaultValue>Roberto Possarle</option>
                                        <option value="3" defaultValue>Ricardo Lemos</option>
                                    </select>
                                </div>
                                <p>
                                    <label>Prontuario</label>
                                    <input type="number" id="idPaciente" value={this.state.idProntuario} onChange={this.atualizarIdProntuario} />
                                </p>
                                <div className="situacao">
                                    <label>Situação</label>
                                    <select name="situação" required value={this.state.situacao} onChange={this.atualizarSituacao}>
                                        <option value="" selected disabled hidden>Selecione</option>
                                        <option value="Agendada" defaultValue>Agendada</option>
                                        <option value="Realizada" defaultValue>Realizada</option>
                                        <option value="Cancelada" defaultValue>Cancelada</option>
                                    </select>   
                                </div>
                                <div className="descricao">
                                    <label>Descrição</label>
                                    <textarea maxLength="400" value={this.state.descricao} onChange={this.atualizarDescricao}></textarea>
                                </div>
                                <input type="submit" value="Cadastrar" />
                                {/* <p className="sucessos" style={{ textAlign: 'center', color: 'red' }}>{this.state.erroMensagem}</p> */}
                                <div className="voltar">
                                    <a href="/App">Voltar</a>
                                </div>
                            </form>
                        </div>
                    </div>
                </main>
            </div>
        );
    }
}

export default CadastroConsulta;