
define(['libs/vue', 'libs/vue-router', 'layout/layout'], function(Vue, VueRouter, Layout) {
    return {
        App: new Vue({
            el: '#app',
            template: ' <div class="wrapper"> \
                            <cben-header></cben-header> \
                            <cben-footer></cben-footer> \
                        </div>',
            data: {
                msg: 'Hello World!',
            },
            components: {
                'cben-header': Layout.Header,
                'cben-footer': Layout.Footer,
            }
        })
    }
});
