
alias gcap="gcap_func"
function gcap_func() {
  git add -A
  git status
  git commit -m "$1"
  git push -u origin "$(git rev-parse --abbrev-ref HEAD)"
}

alias gst="git status"
function gst_func() {
  git status
}
alias gco="git checkout"
function gco_func() {
  git checkout "$1"
}
alias gd="git diff"
function gd_func() {
  git diff "$1"
}

alias gp="git pull"
function gp_func() {
  git pull
}

