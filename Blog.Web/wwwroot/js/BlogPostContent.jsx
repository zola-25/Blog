import React from 'react';
import PropTypes from 'prop-types';

class BlogPostContent extends React.Component {

    createMarkup() {
        return {__html: this.props.blogPost.html};
    }
    render() {
        const {blogPost} = this.props;
        return (
            <div className="blogPost">
                <h1>{blogPost.title}</h1>
                <div dangerouslySetInnerHTML={this.createMarkup()} />
            </div>
        );
    }
}

BlogPostContent.propTypes = {
    blogPost: PropTypes.shape({
        title: PropTypes.string,
        html: PropTypes.string
    })
};



export default BlogPostContent;