module.exports = {
    mode: "development",
    entry: "./wwwroot/js/renderServer.jsx",
    target: 'node',
    output: {
        path: __dirname + "/wwwroot/dist",
        filename: "bundle.dev.js",
        sourceMapFilename: 'bundle.dev.map',
        libraryTarget: 'commonjs'
    },
    devtool: '#source-map',
    module: {
        rules: [{
            test: /\.jsx$/,
            loader: ['babel-loader']
        }]
    }
}