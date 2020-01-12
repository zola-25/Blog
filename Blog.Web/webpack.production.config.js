module.exports = {
    mode: "production",
    entry: "./wwwroot/js/renderServer.jsx",
    output: {
        path: __dirname + "/wwwroot/dist",
        filename: "bundle.prod.js",
        sourceMapFilename: 'bundle.prod.map'
    },
    devtool: '#source-map',
    module: {
        rules: [{
            test: /\.jsx$/,
            loader: ['babel-loader']
        }]
    }
}