import React from 'react';
import PropTypes from 'prop-types';


class BlogPostLinkList extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            expanded: false
        };
        this.expandToggle = this.expandToggle.bind(this);
    }

    expandToggle() {
        const isExpanded = this.state.expanded;
        this.setState({expanded: !isExpanded});
    }
  render() {
      const {blogPostLinks} = this.props;
      const {expanded} = this.state;
      return (
        <div className="mostRecent">
            <h4>Previous Posts</h4>
            <div className="post-list">
                <ul>
                    {
                        blogPostLinks.map((blogPost, i) => {
                                return (i < 5 || expanded ? 
                                <li key={i} >
                                    <a className={`post-list-item`} href={`${blogPost.url}`}>{`${blogPost.title} - ${blogPost.creationDate}`}</a>
                                </li> : ''
                                    );
                            }
                        )
                    }
        
                      <li><a className="view-all" onClick={() => this.expandToggle()} href="javascript::void"><i>{expanded ?  'collapse' : '..view all' }</i></a></li>
                </ul>
            </div>      
        </div>
    );
  }
}

BlogPostLinkList.propTypes = {
    blogPostLinks: PropTypes.arrayOf(PropTypes.object)
};


export default BlogPostLinkList;