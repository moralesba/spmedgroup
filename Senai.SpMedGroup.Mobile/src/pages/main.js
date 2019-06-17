
import React, { Component } from "react";
import { View, Text, TouchableOpacity, StyleSheet } from "react-native";

class Main extends Component {

  render() {
    return (
      <View style={{ flex: 1 }}>
        <View style={styles.overlay} />
        <View style={styles.main}>
          <TouchableOpacity style={styles.instructions} onPress={() => this.props.navigation.navigate('listaConsultas')}>
            <Text style={styles.instructionsText}>Lista de Consultas</Text>
          </TouchableOpacity>
          <TouchableOpacity style={styles.instructions} onPress={() => this.props.navigation.navigate('listaUsuarios')}>
            <Text style={styles.instructionsText}>Lista de Usuarios</Text>
          </TouchableOpacity>
          <TouchableOpacity style={styles.instructions} onPress={() => this.props.navigation.navigate('cadastroConsultas')}>
            <Text style={styles.instructionsText}>Cadastro de Consultas</Text>
          </TouchableOpacity>
          <TouchableOpacity style={styles.instructions} onPress={() => this.props.navigation.navigate('cadastroUsuarios')}>
            <Text style={styles.instructionsText}>Cadastro de Usuarios</Text>
          </TouchableOpacity>
        </View>
      </View>
    );
  }
}
export default Main;

const styles = StyleSheet.create({
  main: {
    flex: 1,
    alignContent: "center",
    alignItems: "center",
    height: "100%",
    justifyContent: 'center',
    width: "100%"
  },

  instructions: {
    alignItems: "center",
    backgroundColor: "#7cc6d3",
    borderColor: "#7cc6d3",
    borderWidth: 1,
    elevation: 1, // Android
    height: 60,
    marginTop: 30,
    justifyContent: "center",
    width: 320,
  },

  instructionsText: {
    fontFamily: "OpenSans-Light",
    fontSize: 17,
    borderBottomWidth: 0,
    letterSpacing: 6,
    marginBottom: 8,
  }
});