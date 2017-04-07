
<template>

<!-- sidebar-->
<aside class="aside">
    <!-- START Sidebar (left)-->
    <div class="aside-inner">
    <nav data-sidebar-anyclick-close="" class="sidebar">
        <!-- START sidebar nav-->
        <ul class="nav">
            <!-- START user info-->
            <li class="has-user-block">
                <div id="user-block" class="collapse">
                <div class="item user-block">
                    <!-- User picture-->
                    <div class="user-block-picture">
                        <div class="user-block-status">
                            <img src="img/user/02.jpg" alt="Avatar" width="60" height="60" class="img-thumbnail img-circle">
                            <div class="circle circle-success circle-lg"></div>
                        </div>
                    </div>
                    <!-- Name and Job-->
                    <div class="user-block-info">
                        <span class="user-block-name">Hello, Mike</span>
                        <span class="user-block-role">Designer</span>
                    </div>
                </div>
                </div>
            </li>
            <!-- END user info-->
            <!-- Iterates over all sidebar items-->
            <template v-for="menu in menus">
                <li class="nav-heading ">
                    <span >{{ menu.Caption }}</span>
                </li>
                <li class=" " v-for="sub in menu.Menus">
                    <a v-bind:href="sub.Href" v-bind:title="sub.Title" data-toggle="collapse">
                        <div v-if="sub.Badge" v-bind:class="[ 'pull-right', 'label', sub.Badge.cssClass ]" v-show="sub.Badge.number != 0">{{ sub.Badge.number }}</div>
                        <em v-bind:class="[ sub.Icon ]"></em>
                        <span >{{ sub.Title }}</span>
                    </a>
                    <ul v-bind:id="sub.Target" class="nav sidebar-subnav collapse">
                        <li class="sidebar-subnav-header">{{ sub.Title }}</li>
                        
                        <li class=" " v-for="l3 in sub.Menus">
                            <a v-bind:href="l3.Href" v-bind:title="l3.Title">
                                <span>{{ l3.Title }}</span>
                            </a>
                        </li>
                    
                    </ul>
                </li>
            </template>
        </ul>
        <!-- END sidebar nav-->
    </nav>
    </div>
    <!-- END Sidebar (left)-->
</aside>

</template>

<script>

import jQuery from 'jQuery'
import AppConfig from './app-config.vue'


var menus = [{
    Caption: 'Main Navigation',
    Menus: [{
        Title: 'Dashboard',
        Icon: 'icon-speedometer',
        Href: '#dashboard',
        Target: 'dashboard',
        Badge: { number: 3, cssClass: 'label-info' },
        Menus: [{
            Title: 'Dashboard v1',
            Href: '#'
        }, {
            Title: 'Dashboard v2',
            Href: '#'
        }, {
            Title: 'Dashboard v3',
            Href: '#'
        }]
    }, {
        Title: 'Widgets',
        Icon: 'icon-grid',
        Href: '#',
        Badge: { number: 20, cssClass: 'label-success' },
    }, {
        Title: 'Layouts',
        Icon: 'icon-layers',
        Href: '#'
    }]
}, {
    Caption: 'Components',
    Menus: [{
        Title: 'Elements',
        Icon: 'icon-chemistry',
        Href: '#components',
        Target: 'components',
        Badge: { number: 0, cssClass: 'label-info' },
        Menus: [{
            Title: 'Dashboard v1',
            Href: '#'
        }, {
            Title: 'Dashboard v2',
            Href: '#'
        }, {
            Title: 'Dashboard v3',
            Href: '#'
        }]
    }, {
        Title: 'Forms',
        Icon: 'icon-note',
        Href: '#',
        Badge: { number: 0, cssClass: 'label-info' },
    }, {
        Title: 'Charts',
        Icon: 'icon-graph',
        Href: '#',
        Badge: { number: 0, cssClass: 'label-info' },
    }, {
        Title: 'Tables',
        Icon: 'icon-grid',
        Href: '#',
        Badge: { number: 0, cssClass: 'label-info' },
    }, {
        Title: 'Maps',
        Icon: 'icon-map',
        Href: '#',
        Badge: { number: 0, cssClass: 'label-info' },
    }]
}, {
    Caption: 'More',
    Menus: [{
        Title: 'Pages',
        Icon: 'icon-doc',
        Href: '#more',
        Target: 'more',
        Badge: { number: 0, cssClass: 'label-info' },
        Menus: [{
            Title: 'Dashboard v1',
            Href: '#'
        }, {
            Title: 'Dashboard v2',
            Href: '#'
        }, {
            Title: 'Dashboard v3',
            Href: '#'
        }]
    }, {
        Title: 'Extras',
        Icon: 'icon-cup',
        Href: '#',
        Badge: { number: 0, cssClass: 'label-info' },
    }, {
        Title: 'Multilevel',
        Icon: 'fa fa-folder-open-o',
        Href: '#',
        Badge: { number: 0, cssClass: 'label-info' },
    }, {
        Title: 'Documentation',
        Icon: 'icon-graduation',
        Href: '#',
        Badge: { number: 0, cssClass: 'label-info' },
    }]
}]


export default {
    data: function() {
        return {
            menus
        }
    },
    mounted: function() {
        (function(window, document, $, undefined){

        var $win;
        var $html;
        var $body;
        var $sidebar;
        var mq;

        $(function(){

            $win     = $(window);
            $html    = $('html');
            $body    = $('body');
            $sidebar = $('.sidebar');
            mq       = AppConfig.APP_MEDIAQUERY;
            
            // AUTOCOLLAPSE ITEMS 
            // ----------------------------------- 

            var sidebarCollapse = $sidebar.find('.collapse');
            sidebarCollapse.on('show.bs.collapse', function(event){

            event.stopPropagation();
            if ( $(this).parents('.collapse').length === 0 )
                sidebarCollapse.filter('.in').collapse('hide');

            });
            
            // SIDEBAR ACTIVE STATE 
            // ----------------------------------- 
            
            // Find current active item
            var currentItem = $('.sidebar .active').parents('li');

            // hover mode don't try to expand active collapse
            if ( ! useAsideHover() )
            currentItem
                .addClass('active')     // activate the parent
                .children('.collapse')  // find the collapse
                .collapse('show');      // and show it

            // remove this if you use only collapsible sidebar items
            $sidebar.find('li > a + ul').on('show.bs.collapse', function (e) {
            if( useAsideHover() ) e.preventDefault();
            });

            // SIDEBAR COLLAPSED ITEM HANDLER
            // ----------------------------------- 


            var eventName = isTouch() ? 'click' : 'mouseenter' ;
            var subNav = $();
            $sidebar.on( eventName, '.nav > li', function() {

            if( isSidebarCollapsed() || useAsideHover() ) {

                subNav.trigger('mouseleave');
                subNav = toggleMenuItem( $(this) );

                // Used to detect click and touch events outside the sidebar          
                sidebarAddBackdrop();
            }

            });

            var sidebarAnyclickClose = $sidebar.data('sidebarAnyclickClose');

            // Allows to close
            if ( typeof sidebarAnyclickClose !== 'undefined' ) {

            $('.wrapper').on('click.sidebar', function(e){
                // don't check if sidebar not visible
                if( ! $body.hasClass('aside-toggled')) return;

                var $target = $(e.target);
                if( ! $target.parents('.aside').length && // if not child of sidebar
                    ! $target.is('#user-block-toggle') && // user block toggle anchor
                    ! $target.parent().is('#user-block-toggle') // user block toggle icon
                ) {
                        $body.removeClass('aside-toggled');          
                }

            });
            }

        });

        function sidebarAddBackdrop() {
            var $backdrop = $('<div/>', { 'class': 'dropdown-backdrop'} );
            $backdrop.insertAfter('.aside').on("click mouseenter", function () {
            removeFloatingNav();
            });
        }

        // Open the collapse sidebar submenu items when on touch devices 
        // - desktop only opens on hover
        function toggleTouchItem($element){
            $element
            .siblings('li')
            .removeClass('open')
            .end()
            .toggleClass('open');
        }

        // Handles hover to open items under collapsed menu
        // ----------------------------------- 
        function toggleMenuItem($listItem) {

            removeFloatingNav();

            var ul = $listItem.children('ul');
            
            if( !ul.length ) return $();
            if( $listItem.hasClass('open') ) {
            toggleTouchItem($listItem);
            return $();
            }

            var $aside = $('.aside');
            var $asideInner = $('.aside-inner'); // for top offset calculation
            // float aside uses extra padding on aside
            var mar = parseInt( $asideInner.css('padding-top'), 0) + parseInt( $aside.css('padding-top'), 0);

            var subNav = ul.clone().appendTo( $aside );
            
            toggleTouchItem($listItem);

            var itemTop = ($listItem.position().top + mar) - $sidebar.scrollTop();
            var vwHeight = $win.height();

            subNav
            .addClass('nav-floating')
            .css({
                position: isFixed() ? 'fixed' : 'absolute',
                top:      itemTop,
                bottom:   (subNav.outerHeight(true) + itemTop > vwHeight) ? 0 : 'auto'
            });

            subNav.on('mouseleave', function() {
            toggleTouchItem($listItem);
            subNav.remove();
            });

            return subNav;
        }

        function removeFloatingNav() {
            $('.sidebar-subnav.nav-floating').remove();
            $('.dropdown-backdrop').remove();
            $('.sidebar li.open').removeClass('open');
        }

        function isTouch() {
            return $html.hasClass('touch');
        }
        function isSidebarCollapsed() {
            return $body.hasClass('aside-collapsed');
        }
        function isSidebarToggled() {
            return $body.hasClass('aside-toggled');
        }
        function isMobile() {
            return $win.width() < mq.tablet;
        }
        function isFixed(){
            return $body.hasClass('layout-fixed');
        }
        function useAsideHover() {
            return $body.hasClass('aside-hover');
        }

        })(window, document, window.jQuery);
    }
}
</script>
