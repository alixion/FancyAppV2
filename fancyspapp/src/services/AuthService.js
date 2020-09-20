import { UserManager, WebStorageStateStore } from "oidc-client";

const STS_DOMAIN = "https://localhost:6101";
const settings = {
  userStore: new WebStorageStateStore({
    store: window.localStorage
  }),
  authority: STS_DOMAIN,
  client_id: "vuejs_code_client",
  redirect_uri: "https://localhost:44359/callback.html",
  automaticSilentRenew: true,
  silent_redirect_uri: "https://localhost:44359/silent-renew.html",
  response_type: "code",
  scope: "openid profile email dataEventRecords",
  post_logout_redirect_uri: "https://localhost:44359/",
  filterProtocolClaims: true
};

let userManager = new UserManager(settings);

export default {
  getUser() {
    return userManager.getUser();
  },

  login() {
    return userManager.signinRedirect();
  },
  logout() {
    return userManager.signoutRedirect();
  },
  getAccessToken() {
    return userManager.getUser().then(data => {
      return data.access_token;
    });
  }
};
