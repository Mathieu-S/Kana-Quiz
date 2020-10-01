const path = require("path");
const HtmlWebpackPlugin = require("html-webpack-plugin");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const TerserPlugin = require("terser-webpack-plugin");
const OptimizeCSSAssetsPlugin = require("optimize-css-assets-webpack-plugin");

module.exports = (env = {}) => ({
  mode: env.prod ? "production" : "development",
  devtool: env.prod ? "source-map" : "eval-cheap-module-source-map",
  entry: {
    app: path.resolve(__dirname, "./scripts/main.ts"),
  },
  output: {
    path: path.resolve(__dirname, "../wwwroot"),
    filename: "js/[name].js",
    chunkFilename: "js/[name].js",
    publicPath: "/",
  },
  module: {
    rules: [
      {
        test: /\.ts?$/,
        use: "ts-loader",
        exclude: /node_modules/,
      },
      {
        test: /\.css$/,
        exclude: /node_modules/,
        use: [
          env.prod ? MiniCssExtractPlugin.loader : "style-loader",
          "css-loader",
          "postcss-loader",
        ],
      },
    ],
  },
  plugins: [
    new HtmlWebpackPlugin({
      template: path.resolve(__dirname, "./index.html"),
    }),
    new MiniCssExtractPlugin({
      filename: "css/[name].css",
    }),
  ],
  optimization: {
    minimize: true,
    minimizer: [new TerserPlugin(), new OptimizeCSSAssetsPlugin()],
  },
});
