
# KQ Analytics 3 Changelog

## Alpha Stage (v0.x-ax)

### v0.6.0-a1 (Due December 26, 2016)

## Development Stage (v0.dx)

### v0.7.0-d [develop]

- TODO: Basic plugin support through Iridium.PluginEngine

### v0.5.1-dev

- Improvement: Use asynchronous Tasks for I/O

### v0.5.0-preview
- Add tracking script for more advanced automated web tracking
  - Log requests for the tracking script
- Cross domain session ID workaround
- Tracking script with automated sending and API
  - Automatically sends hit event, without needing to configure tracking pixel
  - Send basic events automatically
  - Inject a JS API allowing creators to trigger custom events in their code, and
  to integrate KQAnalytics3 into their web application

### v0.4.1
- Patch bugfix: configuration file is now optional

### v0.4.0-dev

- Major new feature: granular API key permission
- Keys in configuration file are now incompatible with previous versions. Each
 key can now be granted a specific set of permissions

### v0.3.1

- Minor bugfix in tag retrieval API

### v0.3.0

- New release! Adds tag data support!

### Previous versions

- Other stuff. Logs were not kept prior to this time.