const path = require("path");

module.exports = {
	entry: {
		app: path.resolve(__dirname, "client-src/app.ts"),
		vendor: path.resolve(__dirname, "client-src/vendor.ts")
	},
	output: {
		path: path.resolve(__dirname, "wwwroot/dist"),
		filename: '[name].bundle.js'
	},
	resolve: {
		extensions: ['.ts', 'tsx', '.js'],
	},
	module: {
		rules: [
			{
				test: /\.(js)$/,
				exclude: /node_modules/,
				use: "babel-loader",
			},
			{
				test: /\.tsx?/,
				exclude: /node_modules/,
				use: ['babel-loader', 'awesome-typescript-loader?silent=true', 'angular2-template-loader', 'angular2-router-loader']
			},
			{
				test: /\.css$/,
				exclude: /node_modules/,
				use: ['to-string-loader', 'style-loader', 'css-loader'],
			},
			{
				test: /\.html$/,
				exclude: /node_modules/,
				loader: 'html-loader'
			},
			{
				test: /\.(jpe?g|png|gif|woff|woff2|eot|ttf|svg)(\?[a-z0-9=.]+)?$/,
				loader: 'url-loader'
			}
		],
	},
	mode: "development",
}