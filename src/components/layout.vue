
<template>
    <header class="topnavbar-wrapper">
        {{ msg }}
    </header>
</template>

<script>
export default {
    name: 'coco-header',
    data: function() {
        return {
            msg: 'this is a header'
        }
    }
}
</script>
