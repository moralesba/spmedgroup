import React, { Component } from "react";
import { Text, StyleSheet, View, FlatList, AsyncStorage } from "react-native";
import api from "../services/api";

class ConsultasMedico extends Component {

    constructor(props) {
        super(props);
        this.state = {
            listaConsultasMedico: []
        };
    }

    componentDidMount() {
        this.ListaConsultasMedico();
    }

    ListaConsultasMedico = async () => {
        this.setState({ loading: true })
        const value = await AsyncStorage.getItem("token-autenticacao")
        const answer = await api.get("/consultas", {
            headers: {
                "Content-Type": "application/json",
                "Authorization": "Bearer " + value
            }
        });

        const dados = answer.data;
        this.setState({ listaConsultasMedico: dados });
        this.setState({ loading: false })
    }

    render() {
        return (
            <View style={styles.main}>
                <View style={styles.mainHeader}>
                    <View style={styles.mainHeaderRow}>
                        <Text style={styles.mainHeaderText}>{"consultas".toUpperCase()}</Text>
                    </View>
                    <View style={styles.mainHeaderLine} />
                </View>

                <View style={styles.mainBody}>
                    <FlatList
                        contentContainerStyle={styles.mainBodyConteudo}
                        data={this.state.listaConsultasMedico}
                        keyExtractor={item => item.idConsulta}
                        renderItem={this.renderizaItem}
                    />
                </View>
            </View>
        );
    }

    renderizaItem = ({ item }) => (
        <View style={styles.main}>
            <View style={styles.flatItem}>
                <View style={styles.flatItemContainer}>
                    <Text style={styles.flatItemTitulo}>ID: {item.idConsulta}</Text>
                    <Text style={styles.flatItemTitulo}>Médico: {item.idMedicoNavigation.nome}</Text>
                    <Text style={styles.flatItemTitulo}>Data: {item.dtConsulta}</Text>
                    <Text style={styles.flatItemTitulo}>Descrição: {item.descricao}</Text>
                    <Text style={styles.flatItemTitulo}>Situação: {item.situacao}</Text>
                </View>
            </View>
        </View>
    );
}

const styles = StyleSheet.create({
    main: {
        flex: 1,
        fontFamily: 'OpenSans-Light',
    },
    mainHeaderRow: {
        flexDirection: "row"
    },
    mainHeader: {
        flex: 1,
        justifyContent: "center",
        alignItems: "center"
    },
    mainHeaderText: {
        fontSize: 16,
        letterSpacing: 5,
        color: "#333",
    },
    mainHeaderLine: {
        width: 170,
        paddingTop: 10,
        borderBottomColor: "#999999",
        borderBottomWidth: 0.9
    },
    mainBody: {
        flex: 4
    },
    mainBodyConteudo: {
        paddingTop: 10,
        paddingRight: 50,
        paddingLeft: 50,
    },
    flatItemTitulo: {
        color: '#333',
        letterSpacing: 1,
        borderBottomColor: "#333",
        borderBottomWidth: .7,
        marginTop: 10
    },
    flatItemContainer: {
        flex: 7,
        marginTop: 10
    },
    flatItem: {
        marginTop: 10,
        marginBottom: 15,
        borderBottomColor: "black",
        borderBottomWidth: 1,
    },
});
export default ConsultasMedico;