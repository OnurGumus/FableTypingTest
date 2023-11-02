import { defineConfig  } from 'vite'

export default defineConfig({
  define: {
    "global": {},
},
  server: {
    watch: {
      ignored: [ "**/*.fs"]
    },

    port: 5183,
    https: false,
    strictPort: true,
    hmr: {
      clientPort: 5183,
      protocol: 'ws'
    }
  }
})