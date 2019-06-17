import React from 'react';
import { Card, Button, Collapse } from 'react-bootstrap'

class Localizacao extends React.Component {
    constructor() {
        super();
        this.state = {
            descricao: false
        };
    }

    render() {
        const dados = this.props.dados;
        const { descricao } = this.state;
        return (
            <Card className="mb-5">
                <Card.Header>
                    {`${dados.nome}, ${dados.idade} ${dados.idade > 1?'anos':'ano'}`}
                </Card.Header>
                <Card.Body>
                    <Card.Title>{"Endereço do paciente"}</Card.Title>
                    <Card.Text>{"Latitude: " + dados.latitude + " - Longitude: " + dados.longitude}</Card.Text>
                    <Card.Title>{"Medico"}</Card.Title>
                    <Card.Text>{"Nome : " + dados.medico.nome}</Card.Text>
                    <Card.Text>{"Especialidade: " + dados.medico.especialidade}</Card.Text>
                    <Button
                        onClick={() => this.setState({ descricao: !descricao })}
                        variant="outline-dark"
                        size="sm"
                        aria-expanded={descricao}
                    >
                        Ver Descrição
                    </Button>
                </Card.Body>
                <Collapse in={this.state.descricao}>
                    <Card.Footer id={"descricao"}>
                        {dados.descricao===""?"Sem descrição":dados.descricao}
                    </Card.Footer>
                </Collapse>
            </Card>
        )
    }
}

export default Localizacao;
