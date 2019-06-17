import React from 'react';
import ReactDOM from 'react-dom';
import * as serviceWorker from './serviceWorker';
import {Route, BrowserRouter as Router, Switch, Redirect} from "react-router-dom";
import ConsultasPaciente from "./pages/consultas/ConsultasPaciente";
import ConsultasMedico from "./pages/consultas/ConsultasMedico";
import Login from "./pages/login/Login";
import App from "./pages/home/App";
import ListarConsultas from './pages/listas/ListarConsultas';
import ListarUsuarios from './pages/listas/ListarUsuarios';
import {UsuarioAutenticado} from "./services/auth";
import {parseJwt} from "./services/auth";
import CadastroUsuario from './pages/cadastros/CadastroUsuario';
import CadastroConsulta from './pages/cadastros/CadastroConsulta';
import { logout } from './services/logout';

// Verifica se é Admin
const PermissaoAdmin = ({component : Component}) => (
    <Route
        render={props =>
            UsuarioAutenticado() && parseJwt().UsuarioTipo == "Administrador" ? (
                <Component {...props}/>
            ) : (
                <Redirect to={{pathname: "/"}}/>
            )
        }
    />
)

// Verifica se é Medico
const PermissaoMedico = ({component : Component}) => (
    <Route
        render={props =>
            UsuarioAutenticado() && parseJwt().UsuarioTipo == "Medico" ? (
                <Component {...props}/>
            ) : (
                <Redirect to={{pathname: "/ConsultasMedico"}}/>
            )
        }
    />
)

// Verifica se é Paciente
const PermissaoPaciente = ({component : Component}) => (
    <Route
        render={props =>
            UsuarioAutenticado() && parseJwt().UsuarioTipo == "Paciente" ? (
                <Component {...props}/>
            ) : (
                <Redirect to={{pathname: "/ConsultasPaciente"}}/>
            )
        }
    />
)


const Routing = (
    <Router>
        <div>
            <Switch>
                <Route exact path="/" component={Login}/>
                <Route exact path="/" component={logout}/>
                <Route path="/CadastroConsulta" component={CadastroConsulta} />
                <Route path="/CadastroUsuario" component={CadastroUsuario} />
                <Route path="/ListarUsuarios" component={ListarUsuarios} />
                <Route path="/App" component={App} />
                <Route path="/ListarConsultas" component={ListarConsultas} />
                <PermissaoPaciente path="/ConsultasPaciente" component={ConsultasPaciente} />
                <PermissaoMedico path="/ConsultasMedico" component={ConsultasMedico} />
                <Route path="/Consultas" component={CadastroConsulta} />
            </Switch>
        </div>
    </Router>
)
ReactDOM.render(Routing, document.getElementById('root'));

serviceWorker.unregister();
