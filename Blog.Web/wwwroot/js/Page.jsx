import React from 'react';
import PropTypes from 'prop-types';
import SideBar from './SideBar.jsx';
import Nav from './Nav.jsx';

class Page extends React.Component {

    render() {
        const { blogPostLinks } = this.props;
        const { isDevelopment } = this.props;

        return (
            <div className="container">
                <div className="row">
                    <div className="col-12">
                        <Nav isDevelopment={isDevelopment} />
                    </div>
                </div>
                <div className="row">
                    <div className="col-sm-9 col-xs-12">
                        <main className="container body-content">

                            {this.props.children}

                        </main>
                    </div>
                    <div className="col-sm-3 col-xs-12">
                        <SideBar blogPostLinks={blogPostLinks} />
                    </div>
                </div>
            </div>

        );
    }
}

export default Page;

