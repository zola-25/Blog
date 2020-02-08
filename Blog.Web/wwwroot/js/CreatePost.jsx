import React from 'react';
import PropTypes from 'prop-types';

class CreatePost extends React.Component {
    componentDidMount() {
        tinymce.init({
            selector: 'textarea',
            plugins: "preview code"
        });
    }
    submit = function(e)
    {
        e.preventDefault();
        fetch("https://api.example.com/items", {
           method: 'post',
           headers: {'Content-Type':'application/json'},
           body: {
            "first_name": this.firstName.value
           }
        }).then(res => res.json())
          .then(
            (result) => {
              this.setState({
                isLoaded: true,
                items: result.items
              });
            },
            // Note: it's important to handle errors here
            // instead of a catch() block so that we don't swallow
            // exceptions from actual bugs in components.
            (error) => {
              this.setState({
                isLoaded: true,
                error
              });
            }
          )
    }
    render() {
        return (
            <form method="post" action="/Admin/Create" onSubmit={this.submit}>
                <div className="form-group">
                    <label htmlFor="Title">Title</label>
                    <input name="Title" className="form-control" placeholder="Title" />
                </div>
                <div className="form-group">
                    <label htmlFor="Permalink">Permalink</label>
                    <input name="Permalink" className="form-control" placeholder="permalink" />
                </div>
                <div className="form-group">
                    <label htmlFor="Html">Content</label>
                    <textarea name="Html" />
                </div>
                <button type="submit" className="btn btn-default">Submit</button>
                <p id="validation-result"></p>

            </form>
        );
    }
}


export default CreatePost;
