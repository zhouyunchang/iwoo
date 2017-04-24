
import Vue from 'vue'
import VueResource from 'vue-resource'

const token_type = 'Bearer'
const token = {
    access_token: '',
    refresh_token: ''
}
const oauthServerOption = {
    authorize_endpoint: 'http://localhost:54443/authorize',
    token_endpoint: 'http://localhost:54443/token',
    client_id: 'CbenWeb',
    client_secret: 'CbenWeb'
}

Vue.http.interceptors.push(function(request, next) {

    if (token.access_token) {
        request.headers.set('Authorization', token_type + ' ' + token.access_token);
    } else {
    
        request.method('GET');
        request.url(oauthServerOption.authorize_endpoint);
        request.params({
            client_id: oauthServerOption.client_id,
            client_secret: oauthServerOption.client_secret
        });
    }

    next();
})

var getToken = () => {
    var kv = location.search.substr(1).split('&');
    for(var i=0; i<kv.length; i++) {
        if (kv[i].split('=')[0] == 'code') {
            _fetchToken(kv[i].split('=')[1]);
        }
    }
}

var _fetchToken = (code) => {
    
}

export default {
}