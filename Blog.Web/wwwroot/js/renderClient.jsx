import React from 'react';
import ReactDOM from 'react-dom';
import ReactDOMServer from 'react-dom/server';
import { createServerRenderer, RenderResult } from 'aspnet-prerendering';

import {BrowserRouter, Route} from 'react-router-dom';

import Main from './Main.jsx';
import BlogPostContent from './blogPostContent.jsx';
import AboutContent from './blogPostContent.jsx';


global.React = React;

const blogPost = {
    title: 'Test post',
    html: '<p>Some content</p>'
};

const data = [{

    url: '/blog/1',
    title: 'blog1',
    creationDate: '4/2/2012'
},{

    url: '/blog/2',
    title: 'blog2',
    creationDate: '4/2/2012'
},{

    url: '/blog/2',
    title: 'blog2',
    creationDate: '4/2/2012'
},{

    url: '/blog/2',
    title: 'blog2',
    creationDate: '4/2/2012'
},{

    url: '/blog/2',
    title: 'blog2',
    creationDate: '4/2/2012'
},{

    url: '/blog/2',
    title: 'blog2',
    creationDate: '4/2/2012'
},{
    
    url: '/blog/2',
    title: 'blog2',
    creationDate: '4/2/2012'
}
    ];

//ReactDOMServer.renderToString(
//    <BrowserRouter>
//        <Route path='/' >
//            <Main blogPostLinks={data} blogPost={blogPost} />
//        </Route>

//    </BrowserRouter>);
export default createServerRenderer(params => {

    return new Promise(function (resolve, reject) {
        var result = ReactDOMServer.renderToString(
            <BrowserRouter>
                <Route path='/' >
                    <Main blogPostLinks={data} >
                        <BlogPostContent blogPost={blogPost} />    
                    </Main>
                </Route>
                <Route path='/About' >
                    <Main blogPostLinks={data} >
                        <AboutContent />    
                    </Main>
                </Route>
            </BrowserRouter>
        );
    
        resolve({ html: result });
    });
});
