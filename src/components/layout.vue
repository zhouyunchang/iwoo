

<template>
    <div class="wrapper">
      
      <top-navbar></top-navbar>
    
      <side-bar></side-bar>

      <off-side-bar></off-side-bar>
      
      <page-body></page-body>
      
      <page-footer></page-footer>
   </div>
</template>

<script>
import TopNavbar from './top-navbar.vue'
import SideBar from './side-bar.vue'
import OffSideBar from './off-side-bar.vue'
import PageFooter from './page-footer.vue'
import PageBody from './page-body.vue'

export default {
    data: function() {
        return {
        }
    },
    components: {
        TopNavbar,
        SideBar,
        PageFooter,
        PageBody,
        OffSideBar
    }
}
</script>
