    
import React from 'react';
import PropTypes from 'prop-types';
import SideBar from './SideBar.jsx';

class Main extends React.Component {

  render() {
      const {blogPostLinks} = this.props;

      return (
        <main className="container body-content">
            <div className="row">
                <div className="col-md-9 col-xs-12">
                      {this.props.children}
                </div>
                <div className="col-md-3 col-xs-12">
                    <SideBar blogPostLinks={blogPostLinks} />
                </div>
            </div>
        </main>
    );
  }
}

export default Main;

