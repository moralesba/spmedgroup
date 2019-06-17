import {
    createBottomTabNavigator,
    createStackNavigator,
    createSwitchNavigator,
    createAppContainer,
    createDrawerNavigator
} from 'react-navigation';

import ConsultasMedico from './pages/consultasMedico';
import ConsultasPaciente from './pages/consultasPaciente';
import Main from './pages/main';
import Login from './pages/login';
import ListaConsultas from './pages/listaConsultas';
import ListaUsuarios from './pages/listaUsuarios';

const AuthStack = createStackNavigator({ Login });

const MedicoDrawerNavigator = createDrawerNavigator({
    ConsultasMedico
})

const PacienteDrawerNavigator = createDrawerNavigator({
    'listaConsultas' : ConsultasPaciente,
    'listaUsuarios' : ListaUsuarios
})

const MainDrawerNavigation = createDrawerNavigator({
    'listaConsultas' : ListaConsultas,
    'listaUsuarios' : ListaUsuarios
})

const MainNavigator = createBottomTabNavigator(
    {
        Main
    },
    {
        initialRouteName: "Main",
        swipeEnabled: true,
        tabBarOptions: {
            showLabel: false,
            showIcon: true,
            inactiveBackgroundColor: "#dd99ff",
            activeBackgroundColor: "#B727FF",
            activeTintColor: "#FFFFFF",
            inactiveTintColor: "#FFFFFF",
            style: {
                height: 50
            }
        }
    }
);
export default createAppContainer(
    createSwitchNavigator(
        {
            MainNavigator,
            AuthStack,
            MedicoDrawerNavigator,
            PacienteDrawerNavigator,
            MainDrawerNavigation
        },
        {
            initialRouteName: "AuthStack"
        }
    )
);