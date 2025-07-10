export type SiteConfig = typeof siteConfig;

export const siteConfig = {
  name: "Revolutionaries",
  description: "Make beautiful websites regardless of your design experience.",
  navItems: [
    {
        label: "Home",
        href: "/",
    },
    {
        label: "My Characters",
        href: "/mycharacters",
    },
    {
        label: "Create New Character",
        href: "/createnewcharacter",
    },
    {
        label: "Compendium",
        href: "/compendium"
    }
  ],
  navMenuItems: [
    {
      label: "Profile",
      href: "/profile",
    },
    {
      label: "Dashboard",
      href: "/dashboard",
    },
    {
      label: "Projects",
      href: "/projects",
    },
    {
      label: "Team",
      href: "/team",
    },
    {
      label: "Calendar",
      href: "/calendar",
    },
    {
      label: "Settings",
      href: "/settings",
    },
    {
      label: "Help & Feedback",
      href: "/help-feedback",
    },
    {
      label: "Logout",
      href: "/logout",
    },
  ],
  links: {
    roll20: "https://app.roll20.net/campaigns/details/18382217/revolutionaries-shifting-sands",
    twitter: "https://twitter.com/hero_ui",
    docs: "https://heroui.com",
    discord: "https://discord.gg/9b6yyZKmH4",
    sponsor: "https://patreon.com/jrgarciadev",
    github: "https://github.com/mullajg/revolutionariesrpg"
  },
};
