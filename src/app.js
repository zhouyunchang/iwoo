
import Vue from 'vue'
import jQuery from 'jquery'
import Layout from './components/layout.vue'

new Vue({
    el: '#app',
    render: function(createElement) {
        return createElement(Layout)
    }
})

