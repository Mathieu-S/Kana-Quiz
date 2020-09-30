const CopyWebpackPlugin = require("copy-webpack-plugin");
const ForkTsCheckerWebpackPlugin = require("fork-ts-checker-webpack-plugin");
const ExtractTextPlugin = require("extract-text-webpack-plugin");
const HtmlWebpackPlugin = require("html-webpack-plugin");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const CompressionWebpackPlugin = require("compression-webpack-plugin");
const UglifyJSPlugin = require("uglifyjs-webpack-plugin");
const WebpackBundleAnalzyer = require("webpack-bundle-analyzer").BundleAnalyzerPlugin;
const WebpackBundleVisualizer = require("webpack-visualizer-plugin");
const path = require("path");
const prod = process.env.NODE_ENV === "production";

const commonPaths = {
  outputPath: path.resolve(__dirname, "./", "assets"),
  contentBasePath: path.resolve(__dirname, "./", "src/public"),
  srcPath: path.resolve(__dirname, "./", "src/js"),
  stylesheetsPath: path.resolve(__dirname, "./", "./stylesheets"),
  bundleVisualizerStatsPath: "./assets/stats",
};
//BundleVisualizer
const currentDateTime = new Date();
const currentDate = currentDateTime.toLocaleDateString("fr-FR").replace(/\//g, "-");
const currentTime = currentDateTime
  .toLocaleTimeString("fr-FR", { hour12: false })
  .replace(/:/g, "-");
const fileDateTime = currentDate + "-" + currentTime;
const filename = path.join(
  commonPaths.bundleVisualizerStatsPath,
  "statistics-" + fileDateTime + ".html"
);
console.log("Generating stats html file here: " + filename);
const config = {
  target: "web",
  devtool: "source-map",
  entry: {
    bundle: commonPaths.srcPath + "/index.ts",
  },
  resolve: {
    extensions: [".ts", ".tsx", ".js"],
  },
  output: {
    filename: "js/[name].[hash].js",
    path: commonPaths.outputPath,
    chunkFilename: "js/[name].[chunkhash:3].js",
  },
  stats: {
    assetsSort: "!size",
    builtAt: false,
    children: false,
    modules: false,
  },
  optimization: {
    splitChunks: {
      chunks: "all",
    },
    minimize: true,
    minimizer: [
      new UglifyJSPlugin({
        sourceMap: true,
        uglifyOptions: {
          compress: {
            global_defs: {},
          },
          beautify: false,
          ecma: 6,
          comments: false,
          mangle: false,
        },
      }),
    ],
  },
  plugins: [
    new ExtractTextPlugin("styles.css", {
      disable: process.env.NODE_ENV === "development",
    }),
    new ForkTsCheckerWebpackPlugin({ checkSyntacticErrors: true }),
    new CopyWebpackPlugin([{ from: "public" }], {
      ignore: ["*.html"],
    }),
    new HtmlWebpackPlugin({
      inject: true,
      template: commonPaths.contentBasePath + "/index.html",
      filename: "index.html",
      minify: {
        removeComments: true,
        collapseWhitespace: true,
        removeRedundantAttributes: true,
        useShortDoctype: true,
        removeEmptyAttributes: true,
        removeStyleLinkTypeAttributes: true,
        keepClosingSlash: true,
        minifyJS: true,
        minifyCSS: true,
        minifyURLs: true,
      },
    }),
    new webpack.DefinePlugin({
      "process.env": {
        NODE_ENV: JSON.stringify("production"),
      },
      DEBUG: false,
      __DEVTOOLS__: false,
    }),
    new MiniCssExtractPlugin({
      filename: commonPaths.stylesheetsPath + "main.css",
      allChunks: true,
    }),
    new CompressionWebpackPlugin({
      filename: "[path].gz[query]",
      algorithm: "gzip",
      test: /\.(js|html|css)$/,
      threshold: 10240,
      minRatio: 0.8,
    }),
    new WebpackBundleAnalzyer(),
    new WebpackBundleVisualizer({ filename }),
  ],
  module: {
    rules: [
      {
        enforce: "pre",
        test: /\.js$/,
        loader: "source-map-loader",
        exclude: ["/node_modules/"],
      },
      {
        test: /\.js$/,
        loader: "babel-loader?cacheDirectory",
      },
      {
        test: /\.ts(x?)$/,
        use: [
          {
            loader: "ts-loader",
            options: {
              happyPackMode: true,
            },
          },
        ],
        include: commonPaths.srcPath,
        exclude: /node_modules/,
      },
      {
        test: /\.css$/i,
        include: commonPaths.stylesheetsPath,
        use: [
          MiniCssExtractPlugin.loader,
          {
            loader: "css-loader",
            options: {
              sourceMap: true,
              importLoaders: 1,
            },
          },
          {
            loader: "postcss-loader",
            options: {
              sourceMap: true,
              plugins: () => [
                require("postcss-import")(),
                require("postcss-nesting")(),
                require("postcss-custom-properties")(),
                require("autoprefixer")(),
              ],
            },
          },
        ],
      },
      {
        test: /\.css$/,
        use: ExtractTextPlugin.extract({
          fallback: "style-loader",
          use: [{ loader: "css-loader", options: { importLoaders: 1 } }, "postcss-loader"],
        }),
      },
      {
        test: /\.css$/i,
        include: commonPaths.srcPath,
        use: [
          MiniCssExtractPlugin.loader,
          "css-modules-typescript-loader",
          {
            loader: "css-loader",
            options: {
              sourceMap: true,
              importLoaders: 1,
              modules: {
                localIdentName: "[name]_[local]_[hash:base64:5]",
              },
              localsConvention: "camelCase",
            },
          },
          {
            loader: "postcss-loader",
            options: {
              plugins: () => [
                require("postcss-import")(),
                require("postcss-nesting")(),
                require("postcss-custom-properties")(),
                require("autoprefixer")(),
              ],
            },
          },
        ],
      },
    ],
  },
};

module.exports = config;
