export const UsuarioAutenticado = () => localStorage.getItem("token-autenticacao") != null;

export const parseJwt = () => {
    var base64Url = localStorage.getItem("token-autenticacao").split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');

    return JSON.parse(window.atob(base64));
}