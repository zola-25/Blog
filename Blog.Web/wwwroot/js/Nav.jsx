import React from 'react';
import PropTypes from 'prop-types';

const Nav = (props) => {
    return (

                    <nav className="navbar navbar-expand navbar-light">
                        <a className="navbar-brand" href="/">Solores Software</a>
                        <ul className="navbar-nav">
                            <li className="nav-item"><a className="nav-link" href="/About">About</a></li>
                            { props.isDevelopment ? (<li className="nav-item"><a className="nav-link" href="/Admin/Index">Admin</a></li>) 
                                : ""}
                        </ul>
                    </nav>

    );
};

Nav.propTypes = {
  isDevelopment: PropTypes.bool
};


export default Nav;

