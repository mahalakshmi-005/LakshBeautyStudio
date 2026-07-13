// ============ LOADING SCREEN ============
window.addEventListener('load', function () {
    var loader = document.getElementById('loading-screen');
    if (loader) {
        setTimeout(function () {
            loader.classList.add('hidden');
        }, 400);
    }
});

// ============ MOBILE NAV TOGGLE ============
document.addEventListener('DOMContentLoaded', function () {
    var toggle = document.getElementById('navToggle');
    var nav = document.getElementById('siteNav');
    if (toggle && nav) {
        toggle.addEventListener('click', function () {
            nav.classList.toggle('open');
        });
        // close menu when a link is clicked
        nav.querySelectorAll('a').forEach(function (link) {
            link.addEventListener('click', function () {
                nav.classList.remove('open');
            });
        });
    }

    // ============ FAQ ACCORDION ============
    var faqItems = document.querySelectorAll('.faq-item');
    faqItems.forEach(function (item) {
        var question = item.querySelector('.faq-question');
        if (question) {
            question.addEventListener('click', function () {
                var isActive = item.classList.contains('active');
                faqItems.forEach(function (i) { i.classList.remove('active'); });
                if (!isActive) {
                    item.classList.add('active');
                }
            });
        }
    });

    // ============ SCROLL REVEAL ANIMATIONS ============
    var revealTargets = document.querySelectorAll('.service-card, .testimonial-card, .offer-card, .counter-item');
    revealTargets.forEach(function (el) {
        el.style.opacity = '0';
        el.style.transform = 'translateY(16px)';
        el.style.transition = 'opacity 0.5s ease, transform 0.5s ease';
    });

    if ('IntersectionObserver' in window) {
        var observer = new IntersectionObserver(function (entries) {
            entries.forEach(function (entry) {
                if (entry.isIntersecting) {
                    entry.target.style.opacity = '1';
                    entry.target.style.transform = 'translateY(0)';
                    observer.unobserve(entry.target);
                }
            });
        }, { threshold: 0.15 });

        revealTargets.forEach(function (el) { observer.observe(el); });
    } else {
        // fallback: no IntersectionObserver support
        revealTargets.forEach(function (el) {
            el.style.opacity = '1';
            el.style.transform = 'none';
        });
    }

    // ============ HEADER SHADOW ON SCROLL ============
    var header = document.getElementById('siteHeader');
    if (header) {
        window.addEventListener('scroll', function () {
            if (window.scrollY > 10) {
                header.style.boxShadow = '0 4px 20px rgba(0,0,0,0.06)';
            } else {
                header.style.boxShadow = 'none';
            }
        });
    }
});
// Admin sidebar mobile toggle
document.addEventListener('DOMContentLoaded', function () {
    var adminToggle = document.getElementById('adminNavToggle');
    var adminSidebar = document.querySelector('.admin-sidebar');
    if (adminToggle && adminSidebar) {
        adminToggle.addEventListener('click', function () {
            adminSidebar.classList.toggle('open');
        });
    }
});

