

define(['libs/vue'], function(Vue) {
    return {
        Header: Vue.component('coco-header', {
            template: requirejs('layout/layout.tmpl')
        }),
        Content: Vue.component('coco-content', {
            template: '<div>content</div>'
        }),
        Footer: Vue.component('coco-footer', {
            template: ' <!-- Page footer--> \
                        <footer> \
                        </footer>'
        }),
    }
})