import { FaPatreon } from "react-icons/fa6";
import { BsStars } from "react-icons/bs";
import { IoSearch } from "react-icons/io5";
import { HiChatAlt2 } from "react-icons/hi";
import { MdNotifications } from "react-icons/md";
import { MdSettingsSuggest } from "react-icons/md";

export const categoryTags = [
    "Art",
    "Podcast",
    "Music",
    "Games",
    "Writing",
    "Photography",
    "Video",
]

export const sidebarIcons = [
    {
        name: FaPatreon,
        size: 20,
        description: ""
    },
    {
        name: BsStars,
        size: 22,
        description: "Recents"
    },
    {
        name: IoSearch,
        size: 22,
        description: "Find Creators"
    },
    {
        name: HiChatAlt2,
        size: 26,
        description: "Community"
    },
    {
        name: MdNotifications,
        size: 24,
        description: "Notifications" 
    },
    {
        name: MdSettingsSuggest,
        size: 24,
        description: "Settings"
    }
]