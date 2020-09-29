class Login   {
    email: string = '';
    password: string = ''

    static Create(): Login {
        return new Login();
    }
}

export default Login;