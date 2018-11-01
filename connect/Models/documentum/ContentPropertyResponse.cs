using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace connect.Models.documentum
{

    public class Properties
    {

        [JsonProperty("object_name")]
        public string object_name { get; set; }

        [JsonProperty("r_object_type")]
        public string r_object_type { get; set; }

        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty("subject")]
        public string subject { get; set; }

        [JsonProperty("authors")]
        public object authors { get; set; }

        [JsonProperty("keywords")]
        public object keywords { get; set; }

        [JsonProperty("a_application_type")]
        public string a_application_type { get; set; }

        [JsonProperty("a_status")]
        public string a_status { get; set; }

        [JsonProperty("r_creation_date")]
        public DateTime r_creation_date { get; set; }

        [JsonProperty("r_modify_date")]
        public DateTime r_modify_date { get; set; }

        [JsonProperty("r_modifier")]
        public string r_modifier { get; set; }

        [JsonProperty("r_access_date")]
        public object r_access_date { get; set; }

        [JsonProperty("a_is_hidden")]
        public bool a_is_hidden { get; set; }

        [JsonProperty("i_is_deleted")]
        public bool i_is_deleted { get; set; }

        [JsonProperty("a_retention_date")]
        public object a_retention_date { get; set; }

        [JsonProperty("a_archive")]
        public bool a_archive { get; set; }

        [JsonProperty("a_compound_architecture")]
        public string a_compound_architecture { get; set; }

        [JsonProperty("a_link_resolved")]
        public bool a_link_resolved { get; set; }

        [JsonProperty("i_reference_cnt")]
        public int i_reference_cnt { get; set; }

        [JsonProperty("i_has_folder")]
        public bool i_has_folder { get; set; }

        [JsonProperty("i_folder_id")]
        public IList<string> i_folder_id { get; set; }

        [JsonProperty("r_composite_id")]
        public object r_composite_id { get; set; }

        [JsonProperty("r_composite_label")]
        public object r_composite_label { get; set; }

        [JsonProperty("r_component_label")]
        public object r_component_label { get; set; }

        [JsonProperty("r_order_no")]
        public object r_order_no { get; set; }

        [JsonProperty("r_link_cnt")]
        public int r_link_cnt { get; set; }

        [JsonProperty("r_link_high_cnt")]
        public int r_link_high_cnt { get; set; }

        [JsonProperty("r_assembled_from_id")]
        public string r_assembled_from_id { get; set; }

        [JsonProperty("r_frzn_assembly_cnt")]
        public int r_frzn_assembly_cnt { get; set; }

        [JsonProperty("r_has_frzn_assembly")]
        public bool r_has_frzn_assembly { get; set; }

        [JsonProperty("resolution_label")]
        public string resolution_label { get; set; }

        [JsonProperty("r_is_virtual_doc")]
        public int r_is_virtual_doc { get; set; }

        [JsonProperty("i_contents_id")]
        public string i_contents_id { get; set; }

        [JsonProperty("a_content_type")]
        public string a_content_type { get; set; }

        [JsonProperty("r_page_cnt")]
        public int r_page_cnt { get; set; }

        [JsonProperty("r_content_size")]
        public int r_content_size { get; set; }

        [JsonProperty("a_full_text")]
        public bool a_full_text { get; set; }

        [JsonProperty("a_storage_type")]
        public string a_storage_type { get; set; }

        [JsonProperty("i_cabinet_id")]
        public string i_cabinet_id { get; set; }

        [JsonProperty("owner_name")]
        public string owner_name { get; set; }

        [JsonProperty("owner_permit")]
        public int owner_permit { get; set; }

        [JsonProperty("group_name")]
        public string group_name { get; set; }

        [JsonProperty("group_permit")]
        public int group_permit { get; set; }

        [JsonProperty("world_permit")]
        public int world_permit { get; set; }

        [JsonProperty("i_antecedent_id")]
        public string i_antecedent_id { get; set; }

        [JsonProperty("i_chronicle_id")]
        public string i_chronicle_id { get; set; }

        [JsonProperty("i_latest_flag")]
        public bool i_latest_flag { get; set; }

        [JsonProperty("r_lock_owner")]
        public string r_lock_owner { get; set; }

        [JsonProperty("r_lock_date")]
        public object r_lock_date { get; set; }

        [JsonProperty("r_lock_machine")]
        public string r_lock_machine { get; set; }

        [JsonProperty("log_entry")]
        public string log_entry { get; set; }

        [JsonProperty("r_version_label")]
        public IList<string> r_version_label { get; set; }

        [JsonProperty("i_branch_cnt")]
        public int i_branch_cnt { get; set; }

        [JsonProperty("i_direct_dsc")]
        public bool i_direct_dsc { get; set; }

        [JsonProperty("r_immutable_flag")]
        public bool r_immutable_flag { get; set; }

        [JsonProperty("r_frozen_flag")]
        public bool r_frozen_flag { get; set; }

        [JsonProperty("r_has_events")]
        public bool r_has_events { get; set; }

        [JsonProperty("acl_domain")]
        public string acl_domain { get; set; }

        [JsonProperty("acl_name")]
        public string acl_name { get; set; }

        [JsonProperty("a_special_app")]
        public string a_special_app { get; set; }

        [JsonProperty("i_is_reference")]
        public bool i_is_reference { get; set; }

        [JsonProperty("r_creator_name")]
        public string r_creator_name { get; set; }

        [JsonProperty("r_is_public")]
        public bool r_is_public { get; set; }

        [JsonProperty("r_policy_id")]
        public string r_policy_id { get; set; }

        [JsonProperty("r_resume_state")]
        public int r_resume_state { get; set; }

        [JsonProperty("r_current_state")]
        public int r_current_state { get; set; }

        [JsonProperty("r_alias_set_id")]
        public string r_alias_set_id { get; set; }

        [JsonProperty("a_effective_date")]
        public object a_effective_date { get; set; }

        [JsonProperty("a_expiration_date")]
        public object a_expiration_date { get; set; }

        [JsonProperty("a_publish_formats")]
        public object a_publish_formats { get; set; }

        [JsonProperty("a_effective_label")]
        public object a_effective_label { get; set; }

        [JsonProperty("a_effective_flag")]
        public object a_effective_flag { get; set; }

        [JsonProperty("a_category")]
        public string a_category { get; set; }

        [JsonProperty("language_code")]
        public string language_code { get; set; }

        [JsonProperty("a_is_template")]
        public bool a_is_template { get; set; }

        [JsonProperty("a_controlling_app")]
        public string a_controlling_app { get; set; }

        [JsonProperty("r_full_content_size")]
        public double r_full_content_size { get; set; }

        [JsonProperty("a_extended_properties")]
        public object a_extended_properties { get; set; }

        [JsonProperty("a_is_signed")]
        public bool a_is_signed { get; set; }

        [JsonProperty("a_last_review_date")]
        public object a_last_review_date { get; set; }

        [JsonProperty("i_retain_until")]
        public object i_retain_until { get; set; }

        [JsonProperty("r_aspect_name")]
        public IList<string> r_aspect_name { get; set; }

        [JsonProperty("i_retainer_id")]
        public object i_retainer_id { get; set; }

        [JsonProperty("i_partition")]
        public int i_partition { get; set; }

        [JsonProperty("i_is_replica")]
        public bool i_is_replica { get; set; }

        [JsonProperty("i_vstamp")]
        public int i_vstamp { get; set; }

        [JsonProperty("business_record")]
        public string business_record { get; set; }

        [JsonProperty("dest_location")]
        public string dest_location { get; set; }

        [JsonProperty("author_creator")]
        public string author_creator { get; set; }

        [JsonProperty("author_date")]
        public DateTime author_date { get; set; }

        [JsonProperty("doc_category")]
        public string doc_category { get; set; }

        [JsonProperty("topic_subject")]
        public string topic_subject { get; set; }

        [JsonProperty("r_object_id")]
        public string r_object_id { get; set; }

        public Properties()
        {

        }
    }

    public class Link
    {

        [JsonProperty("rel")]
        public string rel { get; set; }

        [JsonProperty("href")]
        public string href { get; set; }

        [JsonProperty("title")]
        public string title { get; set; }
    }

    public class ContentPropertyResponse
    {

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("definition")]
        public string definition { get; set; }

        [JsonProperty("properties")]
        public Properties properties { get; set; }

        [JsonProperty("links")]
        public IList<Link> links { get; set; }
    }
}