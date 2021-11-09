import React, { Component } from 'react';
import { NavLink } from 'react-router-dom';
import authService from './AuthorizeService';
import { ApplicationPaths } from './ApiAuthorizationConstants';

export class LoginMenu extends Component {
    constructor(props) {
        super(props);

        this.state = {
            isAuthenticated: false,
            userName: null
        };
    }

    componentDidMount() {
        this._subscription = authService.subscribe(() => this.populateState());
        this.populateState();
    }

    componentWillUnmount() {
        authService.unsubscribe(this._subscription);
    }

    async populateState() {
        const [isAuthenticated, user] = await Promise.all([authService.isAuthenticated(), authService.getUser()])
        this.setState({
            isAuthenticated,
            userName: user && user.name
        });
    }

    render() {
        const { isAuthenticated, userName } = this.state;
        if (!isAuthenticated) {
            const registerPath = `${ApplicationPaths.Register}`;
            const loginPath = `${ApplicationPaths.Login}`;
            return this.anonymousView(registerPath, loginPath);
        } else {
            const profilePath = `${ApplicationPaths.Profile}`;
            const logoutPath = { pathname: `${ApplicationPaths.LogOut}`, state: { local: true } };
            return this.authenticatedView(userName, profilePath, logoutPath);
        }
    }

    authenticatedView(userName, profilePath, logoutPath) {
        return (<div className="p-4 flex items-center justify-between">
            <NavLink className="text-dark" to={profilePath}>Hello {userName}</NavLink>
            <NavLink className="text-dark bg-gray-700 px-3 py-1" to={logoutPath}>Logout</NavLink>
        </div>);

    }

    anonymousView(registerPath, loginPath) {
        return (<div className="p-4 flex items-center justify-between">
            <NavLink className="text-dark bg-gray-700 px-3 py-1" to={registerPath}>Register</NavLink>
            <NavLink className="text-dark bg-gray-700 px-3 py-1" to={loginPath}>Login</NavLink>
        </div>);
    }
}
