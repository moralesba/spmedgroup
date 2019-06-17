
import React, { Component } from 'react';
import { Image, StyleSheet, Text, TextInput, TouchableOpacity, View, AsyncStorage } from 'react-native';
import api from '../services/api';
import jwt from "jwt-decode";

class Login extends Component {

  static navigationOptions = {
    header: null
  };

  constructor(props) {
    super(props);
    this.state = {
      email: "adm@gmail.com",
      senha: "adm123456"
    };
  }

  _realizarLogin = async () => {

    const resposta = await api.post('/login', {
      email: this.state.email,
      senha: this.state.senha
    });

    const token = resposta.data.token;  
    await AsyncStorage.setItem('token-autenticacao', token);

    if (jwt(token).Permissao == 'Medico') {
      this.props.navigation.navigate('MedicoDrawerNavigator')
    }
    else {
      if (jwt(token).Permissao == 'Paciente') {
        this.props.navigation.navigate('PacienteDrawerNavigator')
      }
      else {
        this.props.navigation.navigate('Main')
      }
    }
  }
  render() {
    return (
      <View style={styles.containerInput}>
        <Image style={styles.containerImg} source={require('../assets/img/logo.png')} />
        <TextInput floatingLabel style={styles.inputLogin} placeholder='EMAIL' onChangeText={email => this.setState({ email })} />
        <TextInput style={styles.inputLogin}
          placeholder='SENHA' secureTextEntry={true} onChangeText={senha => this.setState({ senha })} />
        <TouchableOpacity style={styles.btnLogin} onPress={this._realizarLogin}>
          <Text style={styles.btnLoginText}>Login</Text>
        </TouchableOpacity>
      </View>
    );
  }
}

const styles = StyleSheet.create({
  containerInput: {
    flex: 1,
    alignItems: 'center',
    justifyContent: 'center'
  },
  inputLogin: {
    fontSize: 18,
    letterSpacing: 4,
    marginBottom: 15,
    width: 240
  },
  containerImg: {
    marginBottom: 60,
  },
  btnLogin: {
    alignItems: "center",
    backgroundColor: "#F5FCFF",
    borderColor: "#F5FCFF",
    borderWidth: 1,
    elevation: 1, // Android
    height: 60,
    marginTop: 60,
    justifyContent: "center",
    width: 200,
  },
  btnLoginText: {
    fontFamily: "OpenSans-Light",
    fontSize: 18,
    borderBottomWidth: 0,
    letterSpacing: 12,
    textTransform: 'uppercase'
  }
});

export default Login;