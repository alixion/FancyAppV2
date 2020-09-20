const fs = require("fs");
const CopyWebpackPlugin = require("copy-webpack-plugin");

module.exports = {
  devServer: {
    https: {
      key: fs.readFileSync("./certs/localhost+2-key.pem"),
      cert: fs.readFileSync("./certs/localhost+2.pem"),
      port: 44359,
      https: true
    },
    public: "https://localhost:44359"
  },
  configureWebpack: {
    plugins: [
      new CopyWebpackPlugin([
        { from: "node_modules/oidc-client/dist/oidc-client.min.js", to: "js" }
      ])
    ]
  }
};
