import React from 'react';
import Links from './Links.jsx';
import BlogPostLinkList from './BlogPostLinkList.jsx';
import SearchBox from './SearchBox.jsx';


class SideBar extends React.Component {
  render() {
      const {blogPostLinks} = this.props;
    return (
        <> 
            <Links />
            <BlogPostLinkList blogPostLinks={blogPostLinks} />
            <SearchBox />
        </>
    );
    }
}


export default SideBar;
