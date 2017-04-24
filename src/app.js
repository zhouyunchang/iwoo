
import Vue from 'vue'
import jQuery from 'jquery'
import Layout from './components/layout.vue'
import VueRouter from 'vue-router'
import router from './router.js'

Vue.use(VueRouter)

new Vue({
    el: '#app',
    router: router,
    render: function(createElement) {
        return createElement(Layout)
    }
})
