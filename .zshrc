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

alias tf="terraform"
alias tfi="terraform init"
alias tfp="terraform plan"
alias tfa="terraform apply"
alias tfd="terraform destroy"

alias tfipa="tfipa_func"
function tfipa_func() {
    echo "Running terraform init..."
    terraform init
    
    if [ $? -eq 0 ]; then
        echo "Running terraform plan..."
        terraform plan
        
        if [ $? -eq 0 ]; then
            echo "Running terraform apply..."
            terraform apply
        else
            echo "Plan failed. Stopping."
            return 1
        fi
    else
        echo "Init failed. Stopping."
        return 1
    fi
}

alias tfpa="tfpa_func"
function tfpa_func() {
    echo "Running terraform plan..."
    terraform plan
    
    if [ $? -eq 0 ]; then
        echo "Running terraform apply..."
        terraform apply
    else
        echo "Plan failed. Stopping."
        return 1
    fi
}