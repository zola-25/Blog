import React from 'react';
import ReactDOM from 'react-dom';
import ReactDOMServer from 'react-dom/server';
import { createServerRenderer, RenderResult } from 'aspnet-prerendering';

import {StaticRouter, Route} from 'react-router-dom';
import { createMemoryHistory } from 'history';

import Main from './Main.jsx';

import BlogPostContent from './blogPostContent.jsx';
import AboutContent from './AboutContent.jsx';

global.React = React;

const history = createMemoryHistory();
const routerContext = {};

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

        const basename = params.baseUrl.substring(0, params.baseUrl.length - 1); // Remove trailing slash.
        const urlAfterBasename = params.url.substring(basename.length);
        const history = createMemoryHistory({
          initialEntries: [urlAfterBasename]
        });


        var result = ReactDOMServer.renderToString(
            <StaticRouter basename={basename} context={routerContext} location={params.location.path}>
                <Route path='/' exact >
                    <Main blogPostLinks={data} >
                        <BlogPostContent blogPost={blogPost} />    
                    </Main>
                </Route>
                <Route path='/About' exact>
                    <Main blogPostLinks={data} >
                        <AboutContent />    
                    </Main>
                </Route>
            </StaticRouter>
        );
    
        resolve({ html: result });
    });
});
