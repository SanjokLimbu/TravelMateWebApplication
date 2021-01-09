//Register ServiceWorker
if ('serviceWorker' in navigator) {
    window.addEventListener('load', function () {
        navigator.serviceWorker.register('SW_Cached_Pages.js').then(function (registration) {
            console.log('ServiceWorker registration succesful with scope: ', registration.scope);
        }, function (error) {
            console.log('ServiceWorker registration failed: ', error);
        });
    });
}