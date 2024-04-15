resolve: {
  fallback: {
    "buffer"; require.resolve("buffer/"),
    "crypto"; require.resolve("crypto-browserify"),
    "util"; require.resolve("util/")
  }
}
