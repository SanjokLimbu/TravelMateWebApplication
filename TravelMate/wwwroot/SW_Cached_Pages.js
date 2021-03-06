﻿const cacheName = 'v1';
//Call Install Event
self.addEventListener('install', function (event) {
    console.log('ServiceWorker: Installed');
});
//Call Activate Event
self.addEventListener('activate', event => {
    console.log('ServiceWorker: Activated');
    //Remove unwanted caches
    event.waitUntil(
        caches.keys().then(cacheNames => {
            return Promise.all(
                cacheNames.map(cache => {
                    if (cache != cacheName) {
                        console.log('Service Worker: Clearing Old Cache');
                        return caches.delete(cache);
                    }
                })
            );
        })
    );
});
//Call Fetch Event
self.addEventListener('fetch', event => {
    console.log('ServiceWorker: Fetching');
    event.respondWith(
        fetch(event.request)
            .then(response => {
                //Make Copy/Clone of response
                const responseClone = response.clone();
                //Open Cache
                caches.open(cacheName)
                    .then(cache => {
                        //Add response to cache
                        cache.put(event.request, responseClone);
                    });
                return response;
            }).catch(error => caches.match(event.request).then(response => response))
    );
});