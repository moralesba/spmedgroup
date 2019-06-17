import React, { Component } from "react";
import { Text, StyleSheet, View, FlatList, AsyncStorage } from "react-native";
import api from "../services/api";

class ListaUsuarios extends Component {

    constructor(props) {
        super(props);
        this.state = {
            listaUsuarios: []
        };
    }

    componentDidMount() {
        this.ListaUsuarios();
    }

    ListaUsuarios = async () => {
        this.setState({ loading: true })
        const value = await AsyncStorage.getItem("token-autenticacao")
        const answer = await api.get("/usuarios", {
            headers: {
                "Content-Type": "application/json",
                "Authorization": "Bearer " + value
            }
        });

        const dados = answer.data;
        this.setState({ listaUsuarios: dados });
        this.setState({ loading: false })
    }

    render() {
        return (
            <View style={styles.main}>
                <View style={styles.mainHeader}>
                    <View style={styles.mainHeaderRow}>
                        <Text style={styles.mainHeaderText}>{"usuarios".toUpperCase()}</Text>
                    </View>
                    <View style={styles.mainHeaderLine} />
                </View>

                <View style={styles.mainBody}>
                    <FlatList
                        contentContainerStyle={styles.mainBodyConteudo}
                        data={this.state.listaUsuarios}
                        keyExtractor={item => item.idUsuarios}
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
                    <Text style={styles.flatItemTitulo}>Nome: {item.nome}</Text>
                    <Text style={styles.flatItemTitulo}>Email: {item.email}</Text>
                    <Text style={styles.flatItemTitulo}>Senha: {item.senha}</Text>
                    <Text style={styles.flatItemTitulo}>Tipo de usuario: {item.tipoUsuarioNavigation.nome}</Text>
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
        flex: 7
    },
    flatItem: {
        marginBottom: 15,
        borderBottomColor: "black",
        borderBottomWidth: 1,
    },
});
export default ListaUsuarios;