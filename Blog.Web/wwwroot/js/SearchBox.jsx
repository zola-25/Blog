import React from 'react';

class SearchBox extends React.Component {
    
    render() {
        return (
            <>
                <label htmlFor="search">Search</label>
                <div className="post-search-box" >
                    <form action="/Search/SearchRedirect" method="get">
                        <div className="form-group">
                            <input name="search" className="form-control" />
                        </div>
                        <input type="submit" className="btn btn-block" value="Search" />
                    </form>
                </div>
            </>

        );
    }
}

export default SearchBox;