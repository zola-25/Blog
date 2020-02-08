import React from 'react';
import ReactDOM from 'react-dom';
import ReactDOMServer from 'react-dom/server';
import { createServerRenderer, RenderResult } from 'aspnet-prerendering';

import {BrowserRouter, Route} from 'react-router-dom';

import BlogPostContent from './blogPostContent.jsx';
import AboutContent from './AboutContent.jsx';
import Page from './Page.jsx';



global.React = React;

const blogPost = {
    title: 'Test post',
    html: '<p>Some content</p>'
};

React.render(
    <BrowserRouter>
        <div>
        <Route path='/' exact >

            <Page blogPostLinks={blogPostLinks} isDevelopment={isDevelopment} >
                <BlogPostContent blogPost={blogPost} />    
            </Page>

        </Route>
        <Route path='/About' exact>
            <Page blogPostLinks={blogPostLinks} isDevelopment={isDevelopment}>
                <AboutContent />    
            </Page>
        </Route>
        </div>
    </BrowserRouter>);
