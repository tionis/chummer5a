SHELL := /bin/sh
.DEFAULT_GOAL := dist

DOTNET ?= dotnet

.PHONY: dist
dist:
	@command -v "$(DOTNET)" >/dev/null
	@command -v zip >/dev/null
	@command -v tar >/dev/null
	@command -v git >/dev/null
	@set -eu; \
	mkdir -p dist; \
	stage=$$(mktemp -d "$(CURDIR)/dist/.package.XXXXXX"); \
	trap 'rm -rf "$$stage"' EXIT HUP INT TERM; \
	$(DOTNET) build Chummer/Chummer.csproj -c Release --nologo -o "$$stage/Chummer"; \
	cp README.md cs_license.txt xml_license.txt "$$stage/Chummer/"; \
	git ls-files -z -- . ':!:Makefile' > "$$stage/source-files"; \
	tar --null -T "$$stage/source-files" -czf "$$stage/Chummer/source.tar.gz" Makefile; \
	(cd "$$stage" && zip -q -r chummer.zip Chummer \
		-x '*.pdb' '*/logs/*' '*/saves/*' '*.chum5' '*.chum5lz' '*.profile'); \
	mv -f "$$stage/chummer.zip" dist/chummer.zip; \
	echo "Created dist/chummer.zip"
