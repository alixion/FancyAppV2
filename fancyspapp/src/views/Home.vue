<template>
  <div class="home">
    <img alt="Vue logo" src="../assets/logo.png" />

    <div class="home">
      <p v-if="isLoggedIn">User: {{ username }}</p>
      <button class="btn" @click="login" v-if="!isLoggedIn">Login</button>
      <button class="btn" @click="logout" v-if="isLoggedIn">Logout</button>
      <button class="btn" @click="getWeather" v-if="isLoggedIn">
        Get Weather
      </button>
      <button class="btn" @click="getHello" v-if="isLoggedIn">
        Say Hello
      </button>
    </div>

    <div v-if="theData">
      <code>{{ theData }}</code>
    </div>
  </div>
</template>

<script>
import AuthService from "@/services/AuthService";
import axios from "axios";

export default {
  name: "Home",
  data() {
    return {
      currentUser: {},
      accessTokenExpired: false,
      isLoggedIn: false,
      theData: {}
    };
  },
  methods: {
    login() {
      AuthService.login();
    },
    logout() {
      AuthService.logout();
    },
    getWeather() {
      AuthService.getAccessToken().then(userToken => {
        axios.defaults.headers.common["Authorization"] = `Bearer ${userToken}`;
        axios
          .get("https://localhost:6203/weather")
          .then(response => {
            this.theData = response.data;
          })
          .catch(error => {
            console.error(error);
          });
      });
    },
    getHello() {
      AuthService.getAccessToken().then(userToken => {
        axios.defaults.headers.common["Authorization"] = `Bearer ${userToken}`;
        axios
          .get("https://localhost:6203/hello")
          .then(response => {
            this.theData = response.data;
          })
          .catch(error => {
            console.error(error);
          });
      });
    }
  },
  computed: {
    username() {
      return this.currentUser;
    }
  },
  mounted() {
    AuthService.getUser().then(user => {
      this.currentUser = user.profile;
      this.accessTokenExpired = user.expired;

      this.isLoggedIn = user !== null && !user.expired;
    });
  }
};
</script>
