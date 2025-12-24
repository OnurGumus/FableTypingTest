import { defineConfig  } from 'vite'

export default defineConfig({
    server: {
        host: true,
        watch: {
          ignored: [ "**/*.fs"]
        }
      },
    optimizeDeps: {
        entries: []
    }
})