
<template>
<!-- top navbar-->
<header class="topnavbar-wrapper">
      <!-- START Top Navbar-->
      <nav role="navigation" class="navbar topnavbar">
      <!-- START navbar header-->
      <div class="navbar-header">
            <a href="#/" class="navbar-brand">
            <div class="brand-logo">
                  <img src="img/logo.png" alt="App Logo" class="img-responsive">
            </div>
            <div class="brand-logo-collapsed">
                  <img src="img/logo-single.png" alt="App Logo" class="img-responsive">
            </div>
            </a>
      </div>
      <!-- END navbar header-->
      <!-- START Nav wrapper-->
      <div class="nav-wrapper">
            <!-- START Left navbar-->
            <ul class="nav navbar-nav">
            <li>
                  <!-- Button used to collapse the left sidebar. Only visible on tablet and desktops-->
                  <a v-on:click="sidebarToggle" href="#" data-trigger-resize="" data-toggle-state="aside-collapsed" class="hidden-xs">
                  <em class="fa fa-navicon"></em>
                  </a>
                  <!-- Button to show/hide the sidebar on mobile. Visible on mobile only.-->
                  <a href="#" data-toggle-state="aside-toggled" data-no-persist="true" class="visible-xs sidebar-toggle">
                  <em class="fa fa-navicon"></em>
                  </a>
            </li>
            <!-- START User avatar toggle-->
            <li>
                  <!-- Button used to collapse the left sidebar. Only visible on tablet and desktops-->
                  <a id="user-block-toggle" href="#user-block" data-toggle="collapse">
                  <em class="icon-user"></em>
                  </a>
            </li>
            <!-- END User avatar toggle-->
            <!-- START lock screen-->
            <li>
                  <a href="lock.html" title="Lock screen">
                  <em class="icon-lock"></em>
                  </a>
            </li>
            <!-- END lock screen-->
            </ul>
            <!-- END Left navbar-->
            <!-- START Right Navbar-->
            <ul class="nav navbar-nav navbar-right">
            <!-- Search icon-->
            <li>
                  <a href="#" data-search-open="" v-on:click="navbarSearchToggle">
                  <em class="icon-magnifier"></em>
                  </a>
            </li>
            <!-- Fullscreen (only desktops)-->
            <li class="visible-lg">
                  <a href="#" data-toggle-fullscreen="">
                  <em class="fa fa-expand"></em>
                  </a>
            </li>
            <!-- START Alert menu-->
            <li class="dropdown dropdown-list">
                  <a href="#" data-toggle="dropdown">
                  <em class="icon-bell"></em>
                  <div class="label label-danger">11</div>
                  </a>
                  <!-- START Dropdown menu-->
                  <ul class="dropdown-menu animated flipInX">
                  <li>
                        <!-- START list group-->
                        <div class="list-group">
                        <!-- list item-->
                        <a href="#" class="list-group-item">
                              <div class="media-box">
                              <div class="pull-left">
                                    <em class="fa fa-twitter fa-2x text-info"></em>
                              </div>
                              <div class="media-box-body clearfix">
                                    <p class="m0">New followers</p>
                                    <p class="m0 text-muted">
                                    <small>1 new follower</small>
                                    </p>
                              </div>
                              </div>
                        </a>
                        <!-- list item-->
                        <a href="#" class="list-group-item">
                              <div class="media-box">
                              <div class="pull-left">
                                    <em class="fa fa-envelope fa-2x text-warning"></em>
                              </div>
                              <div class="media-box-body clearfix">
                                    <p class="m0">New e-mails</p>
                                    <p class="m0 text-muted">
                                    <small>You have 10 new emails</small>
                                    </p>
                              </div>
                              </div>
                        </a>
                        <!-- list item-->
                        <a href="#" class="list-group-item">
                              <div class="media-box">
                              <div class="pull-left">
                                    <em class="fa fa-tasks fa-2x text-success"></em>
                              </div>
                              <div class="media-box-body clearfix">
                                    <p class="m0">Pending Tasks</p>
                                    <p class="m0 text-muted">
                                    <small>11 pending task</small>
                                    </p>
                              </div>
                              </div>
                        </a>
                        <!-- last list item-->
                        <a href="#" class="list-group-item">
                              <small>More notifications</small>
                              <span class="label label-danger pull-right">14</span>
                        </a>
                        </div>
                        <!-- END list group-->
                  </li>
                  </ul>
                  <!-- END Dropdown menu-->
            </li>
            <!-- END Alert menu-->
            <!-- START Offsidebar button-->
            <li>
                  <a href="#" data-toggle-state="offsidebar-open" data-no-persist="true">
                  <em class="icon-notebook"></em>
                  </a>
            </li>
            <!-- END Offsidebar menu-->
            </ul>
            <!-- END Right Navbar-->
      </div>
      <!-- END Nav wrapper-->
      
      <!-- START Search form-->
      <form role="search" action="search.html" class="navbar-form">
            <div class="form-group has-feedback">
            <input v-on:keyup="navbarSearchInput" v-on:click="$event.stopPropagation()" type="text" placeholder="Type and hit enter ..." class="form-control">
            <div data-search-dismiss="" class="fa fa-times form-control-feedback" v-on:click="navbarSearchDismiss"></div>
            </div>
            <button type="submit" class="hidden btn btn-default">Submit</button>
      </form>
      <!-- END Search form-->

      </nav>
      <!-- END Top Navbar-->
</header>

</template>

      

<script>

import $ from 'jquery'
import StateToggler from './state-toggler.js'
import Vue from 'vue'

var toggle = new StateToggler();

export default {
    data: function() {
        return {
              navbarFormSelector: 'form.navbar-form'
        }
    },
    methods: {
      sidebarToggle: function(event) {
            var element = $(event.target);
            var value = element.data('triggerResize')
            setTimeout(function() {
                  // all IE friendly dispatchEvent
                  var evt = document.createEvent('UIEvents');
                  evt.initUIEvent('resize', true, false, window, 0);
                  window.dispatchEvent(evt);
                  // modern dispatchEvent way
                  // window.dispatchEvent(new Event('resize'));
            }, value || 300);
      }, 
      navbarSearchToggle: function(event) {
            event.stopPropagation();

            var navbarForm = $(this.navbarFormSelector);

            navbarForm.toggleClass('open');
      
            var isOpen = navbarForm.hasClass('open');
      
            navbarForm.find('input')[isOpen ? 'focus' : 'blur']();
      },
      navbarSearchDismiss: function(event) {
            if (event && event.stopPropagation)
                  event.stopPropagation();
            
            $(this.navbarFormSelector)
                  .removeClass('open')      // Close control
                  .find('input[type="text"]').blur() // remove focus
                  .val('')                    // Empty input
                  ;
      }, 
      navbarSearchInput: function(event) {
            if (event.keyCode == 27) // ESC
                  this.navbarSearchDismiss();
      }
    },
    mounted: function() {

      $(document).on('click', this.navbarSearchDismiss);

      (function() {
            var $body = $('body');

            $('[data-toggle-state]')
                  .on('click', function (e) {
                  // e.preventDefault();
                  e.stopPropagation();
                  var element = $(this),
                        classname = element.data('toggleState'),
                        target = element.data('target'),
                        noPersist = (element.attr('data-no-persist') !== undefined);

                  // Specify a target selector to toggle classname
                  // use body by default
                  var $target = target ? $(target) : $body;

                  if(classname) {
                        if( $target.hasClass(classname) ) {
                              $target.removeClass(classname);
                              if( ! noPersist)
                              toggle.removeState(classname);
                        }
                        else {
                              $target.addClass(classname);
                              if( ! noPersist)
                              toggle.addState(classname);
                        }
                  }
                  // some elements may need this when toggled class change the content size
                  // e.g. sidebar collapsed mode and jqGrid
                  $(window).resize();
            })
      })();
    }
}
</script>